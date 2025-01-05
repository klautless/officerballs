extends Node

var timecheck = 0
var loadedin = false
var plactor

var timetarget = 0
var mailcheck = 0

var play_options = []
var playable = 0
var playindex = 0
var playhighlighted = -1


var quantcheck = -3
var inventory = []
var mailouts = []


func _ready(): pass

func _process(delta):

	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0:
		loadedin = false
		plactor = null
		return

	if timecheck > 0:
		timecheck -= 1
	elif timecheck <= 0:
		timecheck = 60 if not loadedin else 300
		for actor in get_tree().get_nodes_in_group("controlled_player"):
			if not is_instance_valid(actor): return
			else:
				if not loadedin:
					plactor = actor
					loadedin = true
		if loadedin:
			_refresh_players()
	if loadedin:
		_get_input()
		timetarget = Time.get_unix_time_from_system()
		
		if timetarget > mailcheck:
			mailcheck = Time.get_unix_time_from_system() + 1.3
			_mailtime()

func _get_input():
	if not loadedin: return
	if not Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_sneak"): plactor.nyan_zoomlock = false
	if plactor.held_item["id"] == "hand_labeler" and not plactor.busy and not plactor.freecamming and not Input.is_action_pressed("move_sneak"):
		if Input.is_action_just_pressed("move_walk") or Input.is_action_just_pressed("move_down"): plactor.nyan_zoomlock = true
		if not Input.is_action_pressed("move_down") and Input.is_action_pressed("move_walk") and Input.is_action_just_released("zoom_in"):
			_quant_pool(true)
		if not Input.is_action_pressed("move_down") and Input.is_action_pressed("move_walk") and Input.is_action_just_released("zoom_out"):
			_quant_pool()
		if Input.is_action_pressed("move_down") and Input.is_action_pressed("move_walk") and Input.is_action_just_released("zoom_in"):
			_player_selector(true)
		if Input.is_action_pressed("move_down") and Input.is_action_pressed("move_walk") and Input.is_action_just_released("zoom_out"):
			_player_selector()
		if Input.is_action_pressed("interact") and not Input.is_action_pressed("move_walk") and Input.is_action_just_pressed("secondary_action"):
			_roll()
		elif Input.is_action_pressed("interact") and Input.is_action_pressed("move_walk") and Input.is_action_just_pressed("secondary_action"):
			_stats()

func _refresh_players():
	if not loadedin: return
	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0: return
	play_options.clear()
	playindex = -1
	var count = 0
	for member in Network.LOBBY_MEMBERS:
		count += 1
		if member["steam_id"] == playhighlighted: playindex = count
		play_options.append({"index": count, "name": member["steam_name"], "id":member["steam_id"]})
	playable = count

func _player_selector(back=false):
	if not loadedin: return
	if playable == 0:
		return
	if not back:
		for member in Network.LOBBY_MEMBERS:
			playindex += 1 
			if playindex < 1 or playindex >= playable+1: playindex = 1
			for i in play_options:
				if int(i["index"]) == int(playindex):
					if not PlayerData.players_hidden.has(i["id"]):# and not PlayerData.players_muted.has(i["id"]):
						var thestring = "fish exchange companion: " + str(i["name"]) + " selected as fish exchange host."
						playhighlighted = i["id"]
						PlayerData._send_notification(str(thestring))
						return
	else:
		for member in Network.LOBBY_MEMBERS:
			playindex -=1
			if playindex <1: playindex = playable
			for i in play_options:
				if int(i["index"]) == int(playindex):
					if not PlayerData.players_hidden.has(i["id"]):# and not PlayerData.players_muted.has(i["id"]):
						var thestring = "fish exchange companion: " + str(i["name"]) + " selected as fish exchange host."
						playhighlighted = i["id"]
						PlayerData._send_notification(str(thestring))
						return

func _quant_pool(back = false):
	if not loadedin: return
	if back: quantcheck -=1
	else: quantcheck += 1
	if quantcheck < 0: quantcheck = 4
	elif quantcheck > 4: quantcheck = 0
	match quantcheck:
		0: PlayerData._send_notification("1 fish")
		1: PlayerData._send_notification("3 fish")
		2: PlayerData._send_notification("7 fish")
		3: PlayerData._send_notification("13 fish")
		4: PlayerData._send_notification("20 fish")

func _process_inventory():
	if not loadedin: return
	inventory.clear()
	var index = 0
	for item in PlayerData.inventory:
		
		if Globals.item_data[item["id"]]["file"].unselectable: continue
		if PlayerData.locked_refs.has(item["ref"]): continue
		if Globals.item_data[item["id"]]["file"].unrenamable: continue
		if Globals.item_data[item["id"]]["file"].category != "fish": continue
		inventory.append(item)
		index += 1

func _mailtime():
	if not loadedin: return
	if PlayerData.inbound_mail.size() == 0 or plactor.busy: return
	for letter in PlayerData.inbound_mail:
		var header = Marshalls.base64_to_utf8(letter["header"])
		var findy = str(header).find("gamba")
		if findy != -1:
			plactor.hud._on_inbox__read_letter(letter)
			return

func _stats():
	if not loadedin: return
	if playhighlighted == -1:
		PlayerData._send_notification("you have to select a valid fish exchange host first!")
		return
	var found = false
	for member in Network.LOBBY_MEMBERS:
		if member["steam_id"] == playhighlighted:
			found = true
	if not found:
		PlayerData._send_notification("the fish exchange host you had selected is no longer in the lobby.")
		return
	PlayerData._send_letter(int(playhighlighted), "gamba", "From, ", "stats", [])
	mailcheck = Time.get_unix_time_from_system() + 1.3
	
func _roll():
	if not loadedin: return
	if playhighlighted == -1:
		PlayerData._send_notification("you have to select a valid fish exchange host first!")
		return
	var found = false
	for member in Network.LOBBY_MEMBERS:
		if member["steam_id"] == playhighlighted:
			found = true
	if not found:
		PlayerData._send_notification("the fish exchange host you had selected is no longer in the lobby.")
		return
	mailouts.clear()
	_process_inventory()
	match quantcheck:
		0:
			if inventory.size() >= 1:
				var ran = randi() % inventory.size()
				mailouts.append(inventory.pop_at(ran))
			else:
				PlayerData._send_notification("not enough fish!")
				return
		1:
			if inventory.size() >= 3:
				for i in 3:
					var ran = randi() % inventory.size()
					mailouts.append(inventory.pop_at(ran))
			else:
				PlayerData._send_notification("not enough fish!")
				return
			
		2:
			if inventory.size() >= 7:
				for i in 7:
					var ran = randi() % inventory.size()
					mailouts.append(inventory.pop_at(ran))
			else:
				PlayerData._send_notification("not enough fish!")
				return
			
		3:
			if inventory.size() >= 13:
				for i in 13:
					var ran = randi() % inventory.size()
					mailouts.append(inventory.pop_at(ran))
			else:
				PlayerData._send_notification("not enough fish!")
				return
			
		4:
			if inventory.size() >= 20:
				for i in 20:
					var ran = randi() % inventory.size()
					mailouts.append(inventory.pop_at(ran))
			else:
				PlayerData._send_notification("not enough fish!")
				return
	
	
	PlayerData._send_letter(int(playhighlighted), "gamba", "From, ", "", mailouts)
	mailcheck = Time.get_unix_time_from_system() + 1.3
