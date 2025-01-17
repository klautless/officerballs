extends Node


#var dedilatch = false
#var internallatch = false

var timecheck = 0
var loadedin = false

#var choice = 0

var spawnID = []

var voidToggle = false
var voidID = []
var voidPos = Vector3()

var tbaitToggle = false
var tbaitPos = Vector3()
var tbaitRot = Vector3()
var tbaitZone = ""
var tbaitZoneOwner = -1


#var meteored = false
#var meteorPos = Vector3()
#var meteorZone = ""
#var meteorZoneOwner = -1

#var hatDisable = true
#var rippleHat = true
#var lockHatPos = false
#var rippleHatPos = Vector3()
#var rippleHeightOffset = -0.32
#var rippleHatZone = ""
#var rippleHatZoneOwner = -1

var plactor = null

var is_host = false
var timetarget = 0
#var rippletimer = 0
#var meteortimer = 0
var baittimer = 0
var voidtimer = 0

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
						is_host = Network.GAME_MASTER
						loadedin = true

func _process(delta):
	if not PlayerAPI.in_game and loadedin:
		loadedin = false
		plactor = null
		tbaitToggle = false
		voidToggle = false
		return
	elif not PlayerAPI.in_game: return

	elif PlayerAPI.in_game and plactor != null:
		timetarget = Time.get_unix_time_from_system()
		_get_input()

		if plactor.direction != Vector3.ZERO and not plactor.lockHatPos:
			for i in 3:
				if spawnID.size() > 0:
					for hat in spawnID:
						if hat.type == "hat":
							plactor._wipe_alt(hat.id, true)
							var actor = plactor.world._get_actor_by_id(hat.id)
							if not is_instance_valid(actor):
								spawnID.erase(hat)
			plactor.dedilatch = false
			plactor.internallatch = false

		if plactor.is_on_floor() and plactor.velocity == Vector3.ZERO and plactor.dive_vec == Vector3.ZERO and not plactor.hatDisable and plactor.dedilatch==false and abs(plactor.spinang) < 50 and not plactor.internallatch:
			plactor.dedilatch = true
			if plactor.internallatch == false:
				if is_host: plactor.rippletimer = 0
				else: _ripple()
			
		if timetarget > baittimer and tbaitToggle: #bait
			baittimer = Time.get_unix_time_from_system() + 14
			if spawnID.size() > 0:
				for bait in spawnID:
					if bait.type == "bait":
						plactor._wipe_alt(bait.id, true)
						spawnID.erase(bait)
						continue
			var baity = Network._sync_create_actor("portable_bait", tbaitPos, tbaitZone, - 1, Network.STEAM_ID, tbaitRot, tbaitZoneOwner)
			spawnID.append({"id": baity, "type":"bait"})

		if is_host:
				
			if timetarget > plactor.meteortimer and plactor.meteored: #meteor
				plactor.meteortimer = Time.get_unix_time_from_system() + 238
				if spawnID.size() > 0:
					for meteor in spawnID:
						if meteor.type == "meteor":
							plactor._wipe_alt(meteor.id, true)
							spawnID.erase(meteor)
							continue
				var new_id = Network._sync_create_actor("fish_spawn_alien", plactor.meteorPos, plactor.meteorZone, - 1, Network.STEAM_ID, Vector3(0,0,0), plactor.meteorZoneOwner)
				spawnID.append({"id": new_id, "type":"meteor"})
			
			if timetarget > voidtimer and voidToggle: #void
				if spawnID.size() > 0:
					voidtimer = Time.get_unix_time_from_system() + 1.3
					for voidy in spawnID:
						if voidy.type == "void":
							plactor._wipe_alt(voidy.id, true)
							var actor = plactor.world._get_actor_by_id(voidy.id)
							if not is_instance_valid(actor):
								spawnID.erase(voidy)
								continue
				else:
					voidtimer = Time.get_unix_time_from_system() + 566
					var newVoid = Network._sync_create_actor("void_portal", voidPos, "main_zone", - 1, Network.STEAM_ID)
					spawnID.append({"id": newVoid, "type": "void"})
			
			if plactor.dedilatch and not plactor.hatDisable and timetarget > plactor.rippletimer: #ripples
				plactor.rippletimer = Time.get_unix_time_from_system() + 55
				if plactor.dedilatch and plactor.rippleHat and not plactor.hatDisable:
					for i in 3:
						if spawnID.size() > 0:
							for hat in spawnID:
								if hat.type == "hat":
									plactor._wipe_alt(hat.id, true)
									var actor = plactor.world._get_actor_by_id(hat.id)
									if not is_instance_valid(actor):
										spawnID.erase(hat)
					if not plactor.lockHatPos:
						plactor.rippleHatZone = plactor.current_zone
						plactor.rippleHatZoneOwner = plactor.current_zone_owner if plactor.current_zone == "island_tiny_zone" or plactor.current_zone == "island_med_zone" or plactor.current_zone == "island_big_zone" else -1
						plactor.rippleHatPos = plactor.global_transform.origin + (Vector3(0, 1.0, 0) * (1.0 - plactor.player_scale))+Vector3(0,plactor.rippleHeightOffset,0)
					var new_id = Network._sync_create_actor("fish_spawn", plactor.rippleHatPos, plactor.rippleHatZone, - 1, Network.STEAM_ID, Vector3(deg2rad(180),0,0), plactor.rippleHatZoneOwner)
					var two_id = Network._sync_create_actor("fish_spawn", plactor.rippleHatPos, plactor.rippleHatZone, - 1, Network.STEAM_ID, Vector3(deg2rad(180),deg2rad(180),0), plactor.rippleHatZoneOwner)
					spawnID.append({"id": new_id, "type": "hat"})
					spawnID.append({"id": two_id, "type": "hat"})
		
		elif not is_host and ((plactor.meteortimer == 0 and plactor.meteored) or (plactor.rippletimer == 0 and not plactor.hatDisable)):
			if plactor.meteortimer == 0:
				plactor.meteortimer = -1
				
				PlayerData._send_notification("metor spot stored bro")
				if spawnID.size() > 0:
					for meteor in spawnID:
						if meteor.type == "meteor":
							plactor._wipe_alt(meteor.id, true)
							spawnID.erase(meteor)
							continue
				var new_id = {"actor_type": "fish_spawn_alien", "at": plactor.meteorPos, "zone": plactor.meteorZone, "actor_id": randi(), "creator_id": Network.STEAM_ID, "rot": Vector3.ZERO, "zone_owner": plactor.meteorZoneOwner}
				plactor.world._instance_actor(new_id)
				new_id = new_id["actor_id"]
				spawnID.append({"id": new_id, "type":"meteor"})
				for meteor in spawnID:
					if meteor.type == "meteor":
						var actor = plactor.world._get_actor_by_id(meteor.id)
						actor.decay = false
			if plactor.rippletimer == 0:
				plactor.rippletimer = -1
				for i in 3:
					if spawnID.size() > 0:
						for hat in spawnID:
							if hat.type == "hat":
								plactor._wipe_alt(hat.id, true)
								var actor = plactor.world._get_actor_by_id(hat.id)
								if not is_instance_valid(actor):
									spawnID.erase(hat)
				var new_id = {"actor_type": "fish_spawn", "at": plactor.rippleHatPos, "zone": plactor.rippleHatZone, "actor_id": randi(), "creator_id": Network.STEAM_ID, "rot": Vector3(deg2rad(180),0,0), "zone_owner": plactor.rippleHatZoneOwner}
				var two_id = {"actor_type": "fish_spawn", "at": plactor.rippleHatPos, "zone": plactor.rippleHatZone, "actor_id": randi(), "creator_id": Network.STEAM_ID, "rot": Vector3(deg2rad(180),deg2rad(180),0), "zone_owner": plactor.rippleHatZoneOwner}
				plactor.world._instance_actor(new_id)
				plactor.world._instance_actor(two_id)
				new_id = new_id["actor_id"]
				two_id = two_id["actor_id"]
				spawnID.append({"id": new_id, "type": "hat"})
				spawnID.append({"id": two_id, "type": "hat"})
				for hat in spawnID:
					if hat.type == "hat":
						var actor = plactor.world._get_actor_by_id(hat.id)
						actor.decay = false

func _ripple(skip=false):
	if plactor.internallatch and not skip: return
	plactor.internallatch = true
	if not plactor.is_on_floor():
		pass
	elif plactor.dedilatch and plactor.rippleHat and not plactor.hatDisable:
		for i in 3:
			if spawnID.size() > 0:
				for hat in spawnID:
					if hat.type == "hat":
						plactor._wipe_alt(hat.id, true)
						var actor = plactor.world._get_actor_by_id(hat.id)
						if not is_instance_valid(actor):
							spawnID.erase(hat)
		if not plactor.lockHatPos:
			plactor.rippleHatZone = plactor.current_zone
			plactor.rippleHatZoneOwner = plactor.current_zone_owner if plactor.current_zone == "island_tiny_zone" or plactor.current_zone == "island_med_zone" or plactor.current_zone == "island_big_zone" else -1
			plactor.rippleHatPos = plactor.global_transform.origin + (Vector3(0, 1.0, 0) * (1.0 - plactor.player_scale))+Vector3(0,plactor.rippleHeightOffset,0)	
		var new_id = {"actor_type": "fish_spawn", "at": plactor.rippleHatPos, "zone": plactor.rippleHatZone, "actor_id": randi(), "creator_id": Network.STEAM_ID, "rot": Vector3(deg2rad(180),0,0), "zone_owner": plactor.rippleHatZoneOwner}
		var two_id = {"actor_type": "fish_spawn", "at": plactor.rippleHatPos, "zone": plactor.rippleHatZone, "actor_id": randi(), "creator_id": Network.STEAM_ID, "rot": Vector3(deg2rad(180),deg2rad(180),0), "zone_owner": plactor.rippleHatZoneOwner}
		plactor.world._instance_actor(new_id)
		plactor.world._instance_actor(two_id)
		new_id = new_id["actor_id"]
		two_id = two_id["actor_id"]
		spawnID.append({"id": new_id, "type": "hat"})
		spawnID.append({"id": two_id, "type": "hat"})
		for hat in spawnID:
			if hat.type == "hat":
				var actor = plactor.world._get_actor_by_id(hat.id)
				actor.decay = false

func _get_input():
	if not Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_sneak"): plactor.nyan_zoomlock = false
	if plactor.held_item["id"] == "tambourine":
		if Input.is_action_just_pressed("move_walk"): plactor.nyan_zoomlock = true
		if Input.is_action_just_pressed("primary_action") and Input.is_action_pressed("move_walk"):
			_tool(plactor.tamb_choice)
		if Input.is_action_just_pressed("interact") and Input.is_action_pressed("move_walk"):
			_tool(plactor.tamb_choice,false,true)
		if Input.is_action_just_released("zoom_in") and Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_sneak"):
			_tool(-1,true)
		if Input.is_action_just_released("zoom_out") and Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_sneak"):
			_tool(-5,true)

func _tool(number,selecting=false,alt=false):
	if selecting:
		if number == -1:
			plactor.tamb_choice += 1
			if plactor.tamb_choice > 3: plactor.tamb_choice = 0
		elif number == -5:
			plactor.tamb_choice -= 1
			if plactor.tamb_choice < 0: plactor.tamb_choice = 3
		match plactor.tamb_choice:
			0: PlayerData._send_notification("rippler selected")
			1: PlayerData._send_notification("meteor selected")
			2: PlayerData._send_notification("permanent bait selected")
			3: PlayerData._send_notification("void portal selected")
		return
	elif not alt:
		match plactor.tamb_choice:
			0:
				plactor.lockHatPos = not plactor.lockHatPos
				if plactor.lockHatPos:
					PlayerData._send_notification("hat pos locked")
				if not plactor.lockHatPos:
					PlayerData._send_notification("hat pos unlocked")
					plactor.dedilatch = true
					_ripple(true)
			1:
				plactor.meteored = not plactor.meteored
				var ver_offset = Vector3(0, 0, 0) * (1.0 - plactor.player_scale)
		
				plactor.meteorPos = plactor.global_transform.origin + (plactor.global_transform.basis.x * + 0.1) + (plactor.global_transform.basis.z * - 2.25) - Vector3(0, 1.75, 0) + ver_offset
				if plactor.meteored:
					plactor.meteorZone = plactor.current_zone
					plactor.meteorZoneOwner = plactor.current_zone_owner if plactor.current_zone == "island_tiny_zone" or plactor.current_zone == "island_med_zone" or plactor.current_zone == "island_big_zone" else -1
					PlayerData._send_notification("metor spot stored bro")
					if spawnID.size() > 0:
						for meteor in spawnID:
							if meteor.type == "meteor":
								plactor._wipe_alt(meteor.id, true)
								spawnID.erase(meteor)
								continue
					if is_host: return
					var new_id = {"actor_type": "fish_spawn_alien", "at": plactor.meteorPos, "zone": plactor.meteorZone, "actor_id": randi(), "creator_id": Network.STEAM_ID, "rot": Vector3.ZERO, "zone_owner": plactor.meteorZoneOwner}
					plactor.world._instance_actor(new_id)
					new_id = new_id["actor_id"]
					spawnID.append({"id": new_id, "type":"meteor"})
					for meteor in spawnID:
						if meteor.type == "meteor":
							var actor = plactor.world._get_actor_by_id(meteor.id)
							actor.decay = false
				if not plactor.meteored:
					if spawnID.size() > 0:
						for meteor in spawnID:
							if meteor.type == "meteor":
								plactor._wipe_alt(meteor.id, true)
								spawnID.erase(meteor)
								continue
					PlayerData._send_notification("auto-meteor disabled")
			2:
				var ver_offset = Vector3(0, 0, 0) * (1.0 - plactor.player_scale)
				tbaitPos = plactor.global_transform.origin + (plactor.global_transform.basis.z * - 2.0) - Vector3(0, 0.9, 0) + ver_offset
				tbaitRot = plactor.rotation + Vector3(0, deg2rad(180), 0)
				tbaitZone = plactor.current_zone
				tbaitZoneOwner = plactor.current_zone_owner if plactor.current_zone == "island_tiny_zone" or plactor.current_zone == "island_med_zone" or plactor.current_zone == "island_big_zone" else -1
				tbaitToggle = not tbaitToggle
				if tbaitToggle:
					PlayerData._send_notification("bait station")
					if spawnID.size() > 0:
						for bait in spawnID:
							if bait.type == "bait":
								plactor._wipe_alt(bait.id, true)
								spawnID.erase(bait)
								continue
					if is_host: return
					var baity = Network._sync_create_actor("portable_bait", tbaitPos, tbaitZone, - 1, Network.STEAM_ID, tbaitRot, tbaitZoneOwner)
					spawnID.append({"id": baity, "type":"bait"})
					for bait in spawnID:
						if bait.type == "bait":
							var actor = plactor.world._get_actor_by_id(bait.id)
							actor.decay = false
				if not tbaitToggle:
					if spawnID.size() > 0:
						for bait in spawnID:
							if bait.type == "bait":
								plactor._wipe_alt(bait.id, true)
								spawnID.erase(bait)
								continue
					PlayerData._send_notification("station gone")
			3:
				var ver_offset = Vector3(0, 0, 0) * (1.0 - plactor.player_scale)
				voidPos = plactor.global_transform.origin + (plactor.global_transform.basis.z * - 2.0) - Vector3(0, 0, 0) + ver_offset
				voidToggle = not voidToggle
				if voidToggle:
					PlayerData._send_notification("hole")
					if spawnID.size() > 0:
						for voidy in spawnID:
							if voidy.type == "void":
								plactor._wipe_alt(voidy.id, true)
								var actor = plactor.world._get_actor_by_id(voidy.id)
								if not is_instance_valid(actor):
									spawnID.erase(voidy)
									continue
					if is_host: return
					var newVoid = {"actor_type": "void_portal", "at": voidPos, "zone": "main_zone", "actor_id": randi(), "creator_id": Network.STEAM_ID, "rot": Vector3.ZERO, "zone_owner": -1}
					plactor.world._instance_actor(newVoid)
					newVoid = newVoid["actor_id"]
					spawnID.append({"id": newVoid, "type": "void"})
					for voidy in spawnID:
						if voidy.type == "void":
							var actor = plactor.world._get_actor_by_id(voidy.id)
							actor.decay = false
				if not voidToggle:
					if spawnID.size() > 0:
						for voidy in spawnID:
							if voidy.type == "void":
								plactor._wipe_alt(voidy.id, true)
								var actor = plactor.world._get_actor_by_id(voidy.id)
								if not is_instance_valid(actor):
									spawnID.erase(voidy)
									continue
					PlayerData._send_notification("hole gone")
	else:
		match plactor.tamb_choice:
			0:
				plactor.hatDisable = not plactor.hatDisable
				if plactor.hatDisable:
					for i in 3:
						if spawnID.size() > 0:
							for hat in spawnID:
								if hat.type == "hat":
									plactor._wipe_alt(hat.id, true)
									var actor = plactor.world._get_actor_by_id(hat.id)
									if not is_instance_valid(actor):
										spawnID.erase(hat)
					plactor.lockHatPos = false
					PlayerData._send_notification("ripple hat disabled")
				else:
					PlayerData._send_notification("ripple hat enabled")
					plactor.dedilatch = true
					_ripple(true)
