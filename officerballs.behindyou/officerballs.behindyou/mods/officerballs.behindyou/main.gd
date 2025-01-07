extends Node

var timecheck = 0
var loadedin = false
var plactor
var punchenabled = false

var play_options = []
var playable = 0
var playindex = 0
var playhighlighted = -1

var PlayerAPI

func _ready():
	PlayerAPI = get_node_or_null("/root/BlueberryWolfiAPIs/PlayerAPI")
	PlayerAPI.connect("_player_added", self, "init_player")
	PlayerAPI.connect("_player_removed", self, "init_player")

func init_player(player: Actor):
	if not loadedin: for i in 5:
		if loadedin: break
		yield (get_tree().create_timer(1),"timeout")
		if get_tree().get_nodes_in_group("controlled_player").size() > 0:
			for actor in get_tree().get_nodes_in_group("controlled_player"):
				if not is_instance_valid(actor): return
				else:
					if not loadedin:
						plactor = actor
						_refresh_players()
						loadedin = true
	else:
		yield (get_tree().create_timer(10),"timeout")
		_refresh_players()

func _process(delta):
	if not PlayerAPI.in_game and loadedin:
		loadedin = false
		plactor = null
		return
	elif not PlayerAPI.in_game: return
	
	elif PlayerAPI.in_game:
		_get_input()

func _get_input():
	if Input.is_action_just_pressed("move_sneak"): plactor.nyan_zoomlock = true
	if not Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_sneak"): plactor.nyan_zoomlock = false
	if Input.is_action_pressed("move_walk") and Input.is_action_pressed("move_sneak") and Input.is_action_just_pressed("secondary_action"):
		punchenabled = not punchenabled
		if punchenabled:
			PlayerData._send_notification("punching enabled")
		else:
			PlayerData._send_notification("punching disabled")
	if Input.is_action_pressed("move_sneak") and Input.is_action_just_pressed("interact") and not Input.is_action_pressed("move_sprint"):
		if play_options.size() > 0:
			for i in play_options:
				if i["index"] == playindex:
					_teleport(i["id"])
	if Input.is_action_pressed("move_sneak") and Input.is_action_just_pressed("move_down"):
		if play_options.size() > 0 and punchenabled:
			for i in play_options:
				if i["index"] == playindex:
					_psyblast(i["id"])
	if Input.is_action_just_released("zoom_in") and Input.is_action_pressed("move_sneak"):
		_player_selector(true)
	if Input.is_action_just_released("zoom_out") and Input.is_action_pressed("move_sneak"):
		_player_selector()
	
func _refresh_players():
	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0: return
	play_options.clear()
	playindex = -1
	var count = 0
	for member in Network.LOBBY_MEMBERS:
		if member["steam_id"] == Network.STEAM_ID: continue
		count += 1
		if member["steam_id"] == playhighlighted: playindex = count
		play_options.append({"index": count, "name": member["steam_name"], "id":member["steam_id"]})
	playable = count

func _player_selector(back=false):
	if playable == 0:
		return
	if not back:
		for member in Network.LOBBY_MEMBERS:
			playindex += 1 
			if playindex < 1 or playindex >= playable+1: playindex = 1
			for i in play_options:
				if int(i["index"]) == int(playindex):
					if not PlayerData.players_hidden.has(i["id"]):# and not PlayerData.players_muted.has(i["id"]):
						var thestring = str(i["name"]) + " selected."
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
						var thestring = str(i["name"]) + " selected."
						playhighlighted = i["id"]
						PlayerData._send_notification(str(thestring))
						return

func _psyblast(id):
	if id == 0: return
	for actor in get_tree().get_nodes_in_group("actor"):
		if actor.actor_type == "player" and actor.owner_id == id and not actor.dead_actor:
			var actor_origin = actor.global_transform.origin
			var dir = (actor_origin - plactor.global_transform.origin).normalized()
			var emupos = actor_origin - (dir * 0.5)
			Network._send_P2P_Packet({"type": "player_punch", "from_pos": emupos, "punch_type": 1}, str(id), 2, Network.CHANNELS.ACTOR_ACTION)

func _teleport(id):
	if not loadedin: return
	if id == 0 or id == -1: return
	for actor in get_tree().get_nodes_in_group("actor"):
		if actor.actor_type == "player" and actor.owner_id == id and not actor.dead_actor:
			if is_instance_valid(actor) and actor.global_transform.origin and actor.current_zone and actor.current_zone_owner:
				var zone = actor.current_zone
				var zone_owner = actor.current_zone_owner
				var ppos = actor.global_transform.origin
				plactor.world._enter_zone(zone, zone_owner)
				plactor.global_transform.origin = ppos
				PlayerData.player_saved_zone = zone
				PlayerData.player_saved_zone_owner = zone_owner
				plactor.last_valid_pos = plactor.global_transform.origin
				return
			else:
				PlayerData._send_notification("they haven't loaded!")
				return
	PlayerData._send_notification("something went wrong!")
	return
