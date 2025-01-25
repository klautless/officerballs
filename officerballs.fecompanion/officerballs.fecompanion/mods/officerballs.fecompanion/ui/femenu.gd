extends Control

var timecheck = 0
var loadedin = false
var plactor = null

var timeTarget = 0
var mailcheck = 0
var maillock = false

var playhighlighted = -1

var internal = 0

var quant = 1
var inventory = []
var mailouts = []

onready var playerlist = $Panel/HBoxContainer/VBoxContainer/playertomail


var PlayerAPI

func _ready():
	PlayerAPI = get_node_or_null("/root/BlueberryWolfiAPIs/PlayerAPI")
	PlayerAPI.connect("_player_added", self, "init_player")
	PlayerAPI.connect("_player_removed", self, "init_player")
	PlayerData.connect("_letter_update", self, "_mail_proxy")
	PlayerData.connect("_inventory_refresh", self, "_process_inventory")

func init_player(player: Actor):
	if not loadedin: for i in 5:
		if loadedin: break
		yield (get_tree().create_timer(1),"timeout")
		if get_tree().get_nodes_in_group("controlled_player").size() > 0:
			for actor in get_tree().get_nodes_in_group("controlled_player"):
				if not is_instance_valid(actor): continue
				else:
					if not loadedin:
						plactor = actor
						playhighlighted = plactor.owner_id
						_refresh_players(true)
						loadedin = true
						yield (get_tree().create_timer(2.5),"timeout")
						_process_inventory()
	else:
		_refresh_players()

func _process(delta):
	if not PlayerAPI.in_game and loadedin:
		loadedin = false
		plactor = null
		return
	elif not PlayerAPI.in_game: return
	elif PlayerAPI.in_game and plactor != null:
		timeTarget = Time.get_unix_time_from_system()
	if plactor.held_item["id"] == "hand_labeler" and Input.is_action_just_pressed("move_down") and $Panel.visible == false and not plactor.busy:
		$Panel.visible = true
	elif plactor.held_item["id"] == "hand_labeler" and Input.is_action_pressed("move_down") and not plactor.busy:
		self.rect_position.x = lerp(self.rect_position.x, 0, delta*10)
	elif self.rect_position.x < 1910:
		self.rect_position.x = lerp(self.rect_position.x, 1920, delta*10)
	elif $Panel.visible == true:
		$Panel.visible = false

func _mail_proxy():
	if timeTarget > internal:
		internal = Time.get_unix_time_from_system() + 1
		maillock = false
		yield (get_tree().create_timer(1), "timeout")
		_mailtime()
	else:
		yield (get_tree().create_timer(1), "timeout")
		_mail_proxy()

func _refresh_players(first=false):
	if not PlayerAPI.in_game or Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0: return
	var indexjump = 0
	var ladiesfirst = 0
	playerlist.clear()
	for member in Network.WEB_LOBBY_MEMBERS:
		var steam_name = Steam.getFriendPersonaName(member)
		playerlist.add_item(steam_name)
		if first and Network.STEAM_ID == member: ladiesfirst = indexjump
		elif playhighlighted == member: ladiesfirst = indexjump
		indexjump += 1
	playerlist.selected = ladiesfirst
	_selectbutton(playerlist.selected)

func _selectbutton(index):
	for member in Network.WEB_LOBBY_MEMBERS:
		var steam_name = Steam.getFriendPersonaName(member)
		if steam_name == playerlist.get_item_text(playerlist.selected):
			playhighlighted = member

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
	
	$Panel/HBoxContainer/VBoxContainer2/invsize.text = "unlocked fish in inv: " + str(index)

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
		PlayerData._send_notification("the fish exchange host you had selected is no longer available.")
		return
	PlayerData._send_letter(int(playhighlighted), "mailer", "From, ", "stats", [])
	mailcheck = Time.get_unix_time_from_system() + 1.5
	
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
	if inventory.size() >= quant:
		for i in quant:
			var ran = randi() % inventory.size()
			mailouts.append(inventory.pop_at(ran))
	else:
		PlayerData._send_notification("not enough fish!")
		return
		
	
	
	PlayerData._send_letter(int(playhighlighted), "mailer", "From, ", "", mailouts)
	mailcheck = Time.get_unix_time_from_system() + 1


func _on_fishcount_value_changed(value):
	$Panel/HBoxContainer/VBoxContainer/quantity.text = "fish to gamble: " + str(value)
	quant = value
