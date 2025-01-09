extends Node

var timecheck = 0
var loadedin = false

var timeTarget = 0

var colorindex = 0
var colortime = 0
var colorspin = false

var rollOver = false
var unlatched = true


var plactor = null

var PlayerAPI

func _ready():
	PlayerAPI = get_node_or_null("/root/BlueberryWolfiAPIs/PlayerAPI")
	PlayerAPI.connect("_player_added", self, "init_player")

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
						loadedin = true
func _process(delta):
	if not PlayerAPI.in_game and loadedin:
		loadedin = false
		plactor = null
		return
	elif not PlayerAPI.in_game: return
	
	elif PlayerAPI.in_game and plactor != null:
		_get_input()
		timeTarget = Time.get_unix_time_from_system()
		
		if colorspin:
			if timeTarget >= colortime:
				colortime = Time.get_unix_time_from_system() + 0.15
				_rainbow_skinner()
		if rollOver:
			plactor.rotation.x = lerp_angle(plactor.rotation.x, deg2rad(75), delta * 7.5)	
		else:
			plactor.rotation.x = lerp_angle(plactor.rotation.x, deg2rad(0), delta * 7.5)
		
func _get_input():
	if not loadedin: return
	var helditem = plactor.held_item
	if not Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_sneak"): plactor.nyan_zoomlock = false
	
	if plactor.held_item["id"] == "chalk_special":
		if Input.is_action_just_pressed("move_walk"): plactor.nyan_zoomlock = true
		if Input.is_action_just_pressed("interact") and Input.is_action_pressed("move_sneak") and Input.is_action_pressed("move_sprint"):
			colorspin = not colorspin
			if not colorspin:
				var new = PlayerData.cosmetics_equipped.duplicate()
				Network._send_actor_action(plactor.actor_id, "_update_cosmetics", [new])
				plactor._update_cosmetics(new)
		if Input.is_action_just_pressed("secondary_action") and Input.is_action_pressed("move_walk") and Input.is_action_pressed("move_sprint") and unlatched:
			rollOver = not rollOver
			unlatched = false
			yield (get_tree().create_timer(0.7), "timeout")
			unlatched = true
		if Input.is_action_just_pressed("interact") and Input.is_action_pressed("move_walk"):
			plactor.helishut = not plactor.helishut
		if Input.is_action_pressed("move_walk") and Input.is_action_just_released("zoom_in"):
			if plactor.spinang == 0:
				plactor.spinfactor = rad2deg(plactor.rotation.y)
				plactor.spinnies = false
			if abs(plactor.spinang) < 1.25: plactor.spinang += 0.125
			elif abs(plactor.spinang) < 5: plactor.spinang += 0.25
			elif abs(plactor.spinang) < 10: plactor.spinang += 0.5
			elif abs(plactor.spinang) < 25: plactor.spinang += 1
			elif abs(plactor.spinang) < 75: plactor.spinang += 2.5
			elif abs(plactor.spinang) < 130: plactor.spinang += 5
			else: plactor.spinang += 10
			if plactor.spinang > 300: plactor.spinang = 300
			var thestring = str(abs(plactor.spinang)) + " spinnies per hour. safe flying."
			PlayerData.emit_signal("_help_update", thestring)
			#plactor.camera_zoom += 0.5
			#plactor._zoom_update()
			if abs(plactor.spinang) > 0: plactor.spinnies = true
		
		if Input.is_action_pressed("move_walk") and Input.is_action_just_released("zoom_out"):
			if plactor.spinang == 0:
				plactor.spinfactor = rad2deg(plactor.rotation.y)
				plactor.spinnies = false
			if abs(plactor.spinang) < 1.25: plactor.spinang -= 0.125
			elif abs(plactor.spinang) < 5: plactor.spinang -= 0.25
			elif abs(plactor.spinang) < 10: plactor.spinang -= 0.5
			elif abs(plactor.spinang) < 25: plactor.spinang -= 1
			elif abs(plactor.spinang) < 75: plactor.spinang -= 2.5
			elif abs(plactor.spinang) < 130: plactor.spinang -= 5
			else: plactor.spinang -= 10
			if plactor.spinang < -300: plactor.spinang = -300
			var thestring = str(abs(plactor.spinang)) + " spinnies per hour. safe flying."
			PlayerData.emit_signal("_help_update", thestring)
			#plactor.camera_zoom -= 0.5
			#plactor._zoom_update()
			if abs(plactor.spinang) > 0: plactor.spinnies = true
	
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
