extends Node


var timecheck = 0
var loadedin = false

var swaying = false
var swayflipflop = false
var sway = 0

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
		if Input.is_action_pressed("move_forward") or Input.is_action_pressed("move_left") or Input.is_action_pressed("move_back") or Input.is_action_pressed("move_right"):
			if not plactor.busy and not plactor.freecamming: swaying = true
			else: swaying = false
		else:
			swaying = false
		if swaying:
			if swayflipflop:
				sway = lerp(sway, -15, delta*6)
				if sway <= -14.8: swayflipflop = false
			else:
				sway = lerp(sway, 17.5, delta*6)
				if sway >= 17.3: swayflipflop = true
		else:
			sway = lerp(sway,deg2rad(0),delta * 8)
		if sway < 0 or sway > 0:
			plactor.rotation.z = lerp_angle(plactor.rotation.z, deg2rad(sway), delta * 8)
		else:
			plactor.rotation.z = lerp_angle(plactor.rotation.z, deg2rad(0), delta * 2)
