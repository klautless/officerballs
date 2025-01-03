extends Node


var timecheck = 0
var loadedin = false
var plactor

var swaying = false
var swayflipflop = false
var sway = 0

func _ready(): pass

func _process(delta):
	
	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0 or get_tree().get_nodes_in_group("controlled_player").size() == 0:
		loadedin = false
		plactor = null
		return
	
	
	if timecheck > 0:
		timecheck -= 1
	elif timecheck <= 0:
		timecheck = 60
		for actor in get_tree().get_nodes_in_group("controlled_player"):
			if not is_instance_valid(actor): return
			else:
				if not loadedin:
					loadedin = true
					plactor = actor
					
	if loadedin:
		if Input.is_action_pressed("move_forward") or Input.is_action_pressed("move_left") or Input.is_action_pressed("move_back") or Input.is_action_pressed("move_right"):
			swaying = true
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
		
		var _speed = 6.4
		if plactor.sprinting: _speed = _speed * plactor.sprint_speed * plactor.boost_mult

		if plactor.direction != Vector3.ZERO and plactor.diving and plactor.is_on_floor():
			var snap_vec = Vector3(0, - 0.4, 0)
			var speed_mult = 1.0
			var _accel = 72
			speed_mult = clamp(speed_mult - ((plactor.held_item_weight / 25.0) * 0.05), 0.15, 1.0)
			plactor.velocity = plactor.velocity.move_toward(plactor.direction.normalized() * _speed * speed_mult, delta * _accel)
			plactor.move_and_slide_with_snap(plactor.velocity + plactor.gravity_vec, snap_vec, Vector3.UP)
