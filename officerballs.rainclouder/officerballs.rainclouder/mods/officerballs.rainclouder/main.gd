extends Node

var timecheck = 0
var loadedin = false
var plactor

var cloudIDs = []

var rainToggle = false
var rainTimer = 0
var rainPos = Vector3()
var rainZone = ""
var rainZoneOwner = -1

func _ready(): pass

func _process(delta):
	
	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0 or get_tree().get_nodes_in_group("controlled_player").size() == 0:
		loadedin = false
		plactor = null
		rainToggle = false
		return
	
	if timecheck > 0:
		timecheck -= 1
	elif timecheck <= 0:
		timecheck = 60
		for actor in get_tree().get_nodes_in_group("controlled_player"):
			if not is_instance_valid(actor): return
			else:
				if not loadedin:
					plactor = actor
					loadedin = true

	if loadedin:
		_get_input()
		if rainTimer > 0:
			rainTimer -= 1
		if rainTimer <= 0:
			rainTimer = 2680
			if rainToggle:
				if cloudIDs.size() > 0:
					for cloud in cloudIDs:
						if cloud.yes == "rainmain":
							plactor._wipe_actor(cloud.id)
							cloudIDs.erase(cloud)
							continue
				var newCloud = Network._sync_create_actor("raincloud_tiny", rainPos, rainZone, - 1, Network.STEAM_ID, Vector3.ZERO, rainZoneOwner)
				cloudIDs.append({"id": newCloud, "yes": "rainmain"})

func _get_input():
	if not loadedin: return
	if Input.is_action_just_pressed("alt_primary") and Input.is_action_pressed("move_sneak"): 
		rainToggle = not rainToggle
		rainPos = plactor.global_transform.origin + Vector3(0,4.5,0)
		if rainToggle:
			PlayerData._send_notification("rain position updated")
			rainZone = plactor.current_zone
			rainZoneOwner = plactor.current_zone_owner if plactor.current_zone == "island_tiny_zone" or plactor.current_zone == "island_med_zone" or plactor.current_zone == "island_big_zone" else -1
			rainTimer = 0
		if not rainToggle:
			PlayerData._send_notification("auto-rainer disabled")
			if cloudIDs.size() > 0:
					for cloud in cloudIDs:
						if cloud.yes == "rainmain":
							plactor._wipe_actor(cloud.id)
							cloudIDs.erase(cloud)
							continue
