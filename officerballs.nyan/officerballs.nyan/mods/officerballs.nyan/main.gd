extends Node

var timecheck = 0
var loadedin = false
var plactor

var colorindex = 0
var colortime = 0
var colorspin = false

func _ready(): pass

func _process(delta):
	
	if timecheck > 0:
		timecheck -= 1
	elif timecheck <= 0:
		timecheck = 60
		if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0:
			loadedin = false
			colorspin = false
			plactor = null
			return
		for actor in get_tree().get_nodes_in_group("controlled_player"):
			if not is_instance_valid(actor): return
			else:
				if not loadedin:
					plactor = actor
					loadedin = true
	if loadedin:
		_get_input()
		if colorspin:
			if colortime > 0:
				colortime -=1
			elif colortime <= 0:
				colortime = 9
				_rainbow_skinner()
			

func _get_input():
	if not loadedin: return
	var helditem = plactor.held_item
	if Input.is_action_just_pressed("interact") and Input.is_action_pressed("move_sneak") and Input.is_action_pressed("move_sprint") and plactor.held_item["id"] == "chalk_special":
		colorspin = not colorspin
		if not colorspin:
			var new = PlayerData.cosmetics_equipped.duplicate()
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
	
func _rainbow_skinner():
	if not loadedin: return
	var new = PlayerData.cosmetics_equipped.duplicate()
	match colorindex:
		0:
			new["primary_color"] = "pcolor_maroon"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		1:
			new["primary_color"] = "pcolor_red"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		2:
			new["primary_color"] = "pcolor_orange"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		3:
			new["primary_color"] = "pcolor_yellow"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		4:
			new["primary_color"] = "pcolor_olive"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		5:
			new["primary_color"] = "pcolor_green"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		6:
			new["primary_color"] = "pcolor_teal"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		7:
			new["primary_color"] = "pcolor_blue"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		8:
			new["primary_color"] = "pcolor_purple"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
		9:
			new["primary_color"] = "pcolor_pink_special"
			Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
			plactor._update_cosmetics(new)
	colorindex += 1
	if colorindex >= 10: colorindex = 0
