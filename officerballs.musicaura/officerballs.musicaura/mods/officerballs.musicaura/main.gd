extends Node

var timecheck = 0
var loadedin = false
var plactor

func _ready(): pass

func _process(delta):
	
	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0:
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
					plactor = actor
					loadedin = true
	if loadedin:
		if randf() < 0.06:
			var a = (randf() - 0.5) * 1.2
			var b = randf() - 0.5
			var c = (randf() - 0.5) * 1.2
			plactor._sync_particle("music",Vector3(a,b,c))
