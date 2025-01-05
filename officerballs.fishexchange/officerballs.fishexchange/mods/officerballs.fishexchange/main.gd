extends Node

var loadedin = false

var gambaenabled = true
var gambascanner = 0
var pitysystem = []
var globalpity = 0
var temprollog = []
var temprolltotal = 0
var gambaautosavetimer = 3600
var gambaleaderboard = []

var timeTarget = 0


func _ready(): pass


func _process(delta):
	
	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0:
		loadedin = false
		return
	
	timeTarget = Time.get_unix_time_from_system()
	
	if gambascanner:
		gambascanner -= 1
	elif gambascanner <= 0:
		gambascanner = 60
		for actor in get_tree().get_nodes_in_group("controlled_player"):
			if not is_instance_valid(actor): return
			else:
				if not loadedin:
					loadedin = true
					_load_gambadata()
				_gamba()

	if loadedin:
		
		if timeTarget > gambaautosavetimer:
			gambaautosavetimer = Time.get_unix_time_from_system() + 60
			if temprollog != []:
				var ourstring = ""
				var lobbynet = 0
				for roll in temprollog:
					var cheeser = int(roll[1])
					lobbynet += int(cheeser)
					ourstring = ourstring + "roll: " + str(roll[0]) + " - $" + str(roll[3]) + " gamba'd / $" + str(roll[4]) + " returned - quality weight: " + str(roll[2]) + " - payout differential " + str(roll[1]) + "\n"
				ourstring = ourstring + "\npity reported: " + str(globalpity) + " - actual lobby net: " + str(lobbynet)
				var gSAVE_PATH = "user://gambling_readout.txt"
				var gsave = File.new()
				gsave.open(gSAVE_PATH, File.WRITE)
				gsave.store_string(ourstring)
				gsave.close()
				yield (get_tree().create_timer(0.25), "timeout")
				
				var gNewSAVE_PATH = "user://gambling_leaderboard.sav"
				var gNewsave = File.new()
				gNewsave.open(gNewSAVE_PATH, File.WRITE)
				gNewsave.store_var(gambaleaderboard)
				gNewsave.close()
				yield (get_tree().create_timer(0.25), "timeout")
				PlayerData._send_notification("gambling leaderboard auto-saved")

func _load_gambadata():
	
	var gNewSAVE_PATH = "user://gambling_leaderboard.sav"
	var gNewsave = File.new()
	
	var gNewsave_exists = gNewsave.file_exists(gNewSAVE_PATH)
	if gNewsave_exists:
		gNewsave.open(gNewSAVE_PATH, File.READ)
		gambaleaderboard = gNewsave.get_var(gambaleaderboard)
		gNewsave.close()
		if gambaleaderboard != []:
			for i in gambaleaderboard:
				i["dolla_in"] = 0
				i["dolla_out"] = 0
				i["dolla_net"] = 0
				i["jackpots"] = 0
				i["fish_in"] = 0
				i["fish_out"] = 0
				i["fish_net"] = 0
		PlayerData._send_notification("gambling leaderboard loaded!")
	else: pass

func _gamba_stats(id):
	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0: return
	
	var hero = id
	
	var stats = ""
	var lbs = ""
	var herofound = false
	var anychecked = false
	if gambaleaderboard != []:
		var firstpass = true
		
		var _tot_dolla_in
		var _tot_dolla_out
		var _tot_dolla_net
		var _tot_jackpots
		
		var _top_jackpot
		
		var _tot_fish_in
		var _tot_fish_out
		var _tot_fish_net
		var tiniest_ever = true
		var _tiniest_yet
		var lorgest_ever = true
		var _lorgest_yet
		
		var _jackpotrank = 1
		var _topjackpotrank = 1
		var _donorrank = 1
		var _dollarrank = 1
		var _fishdonorrank = 1
		var _fishearnerrank = 1
		
		for i in 2:
			for competition in gambaleaderboard:
				if competition["id"] != hero and firstpass: continue
				elif firstpass and competition["id"] == hero:
					herofound = true
					firstpass = false
					_tot_dolla_in = competition["tot_dolla_in"]
					_tot_dolla_out = competition["tot_dolla_out"]
					_tot_dolla_net = competition["tot_dolla_net"]
					_tot_jackpots = competition["tot_jackpots"]
					_top_jackpot = competition["highest_jackpot"]
					_tot_fish_in = competition["tot_fish_in"]
					_tot_fish_out = competition["tot_fish_out"]
					_tot_fish_net = competition["tot_fish_net"]
					_tiniest_yet = competition["tiniest_yet"]
					_lorgest_yet = competition["lorgest_yet"]
					break
				anychecked = true
				var tot_dolla_in = competition["tot_dolla_in"]
				var tot_dolla_out = competition["tot_dolla_out"]
				var tot_dolla_net = competition["tot_dolla_net"]
				var tot_jackpots = competition["tot_jackpots"]
				var top_jackpot = competition["highest_jackpot"]
				var tot_fish_in = competition["tot_fish_in"]
				var tot_fish_out = competition["tot_fish_out"]
				var tot_fish_net = competition["tot_fish_net"]
				var tiniest_yet = competition["tiniest_yet"]
				var lorgest_yet = competition["lorgest_yet"]
				if tot_jackpots > _tot_jackpots: _jackpotrank +=1
				if top_jackpot > _top_jackpot: _topjackpotrank += 1
				if tot_dolla_in > _tot_dolla_in: _donorrank += 1
				if tot_dolla_net > _tot_dolla_net: _dollarrank += 1
				if tot_fish_in > _tot_fish_in: _fishdonorrank += 1
				if tot_fish_net > _tot_fish_net: _fishearnerrank += 1
				if tiniest_yet < _tiniest_yet and tiniest_yet != 0: tiniest_ever = false
				if lorgest_yet > _lorgest_yet: lorgest_ever = false
		if anychecked:
			lbs = lbs + "#" + str(_topjackpotrank) + " in highest jackpot won. #" + str(_jackpotrank) + " in most jackpots won. #" + str(_donorrank) + " in most dollars gamba'd. #" + str(_dollarrank) + " in net dollars earned. #" + str(_fishdonorrank) + " in most fish gamba'd. #" + str(_fishearnerrank) + " in net fish earned. " 
			if tiniest_ever: lbs = lbs + "winner of the tiniest fish from fish exchange. "
			if lorgest_ever: lbs = lbs + "winner of the lorgest fish from fish exchange."
		if herofound:
			var flipnet3 = ""
			var flipnet4 = ""

			if _tot_dolla_net < 0:
				_tot_dolla_net = abs(_tot_dolla_net)
				flipnet3 = "-"
			if _tot_fish_net < 0:
				_tot_fish_net = abs(_tot_fish_net)
				flipnet4 = "-"
			
			stats = "lifetime stats: " + str(_tot_jackpots) + " jackpots, highest jackpot $" + str(_top_jackpot) + ". $" + str(_tot_dolla_in) + " gamba'd, " + flipnet3 + "$" + str(_tot_dolla_net) + " net earnings - " + str(_tot_fish_in) + " fish gamba'd, " + flipnet4 + str(_tot_fish_net) + " earned."
	
	if herofound:
		var mname = ""
		for member in Network.LOBBY_MEMBERS:
			if int(id) == int(member["steam_id"]):
				mname = "stats mail delivered to " + member["steam_name"]
		stats = stats + "\n" + lbs
		randomize()
		var letter_id = randi()
		var ref = randi()
		var returndata = {"letter_id": letter_id, "header": "gamba", "closing": "From, ", "body": stats, "items": [], "to": str(id), "from": str(Network.STEAM_ID), "user": str(Network.STEAM_USERNAME)}
		if id != Network.STEAM_ID:
			Network._send_P2P_Packet({"type": "letter_recieved", "data": returndata, "to": str(id)}, str(id), 2, Network.CHANNELS.GAME_STATE)
		else :
			PlayerData._recieved_letter(returndata)
		
		PlayerData._send_notification(str(mname))

func _gamba():
	
	if PlayerData.inbound_mail.size() < 1: return
	
	for letter in PlayerData.inbound_mail:
		var actualitems = 0
		var getlucky = false
		var items = []
		if letter["items"] is Array:
			var findsender = ""
			var tk2 = ""
			var is1_encoded = PlayerData._is_base64_encoded(letter["header"])
	
			if is1_encoded:
				tk2 = Marshalls.base64_to_utf8(letter["header"])
				print("Letter is Base64")
			else :
				print("Letter is Legacy")
				
			var is2_encoded = PlayerData._is_base64_encoded(letter["body"])
	
			if is2_encoded:
				findsender = Marshalls.base64_to_utf8(letter["body"])
				print("Letter is Base64")
			else :
				print("Letter is Legacy")
			
			var whogets = 0
			
			for member in Network.LOBBY_MEMBERS:
				if letter["from"] == member["steam_name"]: whogets = member["steam_id"]
			
			if whogets == 0: continue
			if PlayerData.players_hidden.has(whogets) or PlayerData.players_muted.has(whogets): continue
			
			var deny = str(findsender).find("$")
			if deny != -1: continue
			
			# is this stats mail?
			var ck1 = str(findsender).find("stats")
			var ck2 = str(tk2).find("stats")
			var testes = ck1 != -1 or ck2 != -1
			if letter["items"] == [] and testes:
				_gamba_stats(whogets)
				PlayerData.inbound_mail.erase(letter)
				PlayerData.emit_signal("_letter_update")
				continue
			
			
			# system enable/disable
			var ek1 = str(findsender).find("toggle")
			var ek2 = str(tk2).find("toggle")
			var toggler = ek1 != -1 or ek2 != -1
			if letter["items"] == [] and toggler and whogets == Network.STEAM_ID:
				gambaenabled = not gambaenabled
				PlayerData.inbound_mail.erase(letter)
				PlayerData.emit_signal("_letter_update")
				if gambaenabled:
					PlayerData._send_notification("gambling enabled")
				else:
					PlayerData._send_notification("gambling disabled")
			elif letter["items"] == []: continue
			
			# progress no further if gambling is disabled
			if not gambaenabled: continue
			
			# choose to lose
			var lk1 = str(findsender).find("lose")
			var lk2 = str(tk2).find("lose")
			var tootsies = lk1 != -1 or lk2 != -1
			
			# cure arthritis
			var ak1 = str(findsender).find("arthritis")
			var ak2 = str(tk2).find("arthritis")
			var thritis = ak1 != -1 or ak2 != -1
			
			randomize()
			var lucklevel = randi() % 7 + 1
			if lucklevel == 7: getlucky = true
			
			var confirmeditems = 0
			var qualityweight = 0
			var stockvalue = 0
			var aftermarket = 0
			for item in letter["items"]:
				var indqual = 0
				item = PlayerData._validate_item_safety(item)
				var file = Globals.item_data[item["id"]]["file"]
				var isit = file["category"]
				if isit != "fish": continue
				if file.unselectable or not file.can_be_sold: continue
				
				# omit placeholder items (modded/fucky fish) and prevent random crap from counting as fish
				if item["id"] == "fish_lake_salmon" and item["size"] == 60.0 and item["quality"] == 0 and item["custom_name"] == "": continue
				if file.item_name == "Spectral Rib" or file.item_name == "Spectral Femur" or file.item_name == "Spectral Humerus" or file.item_name == "Spectral Skull" or file.item_name == "Spectral Spine" or file.item_name == "Button" or file.item_name == "Casing" or file.item_name == "Old Coin" or file.item_name == "Hat Fragment" or file.item_name == "Monocle Fragment" or file.item_name == "Blade Fragment" or file.item_name == "Watch Fragment" or file.item_name == "Rusted Ring" or file.item_name == "Soda Tab": continue
				
				# adapted PlayerData._get_item_worth since that checks player inventory vs. mail-ins
				# tried patching the _get_item_worth function itself to take an additional input and bypass the _find_item_code within; no dice
				var size_prefix = {
					0.1: 1.75, 
					0.25: 0.6, 
					0.5: 0.8, 
					1.0: 1.0, 
					1.5: 1.5, 
					2.0: 2.5, 
					3.0: 4.25, 
				}
			
				var average = Globals.item_data[item["id"]]["file"].average_size
				var calc = item["size"] / average
				var mult = 1.0
					
				for p in size_prefix.keys():
					if p > calc: break
					mult = size_prefix[p]
					
				var idata = Globals.item_data[item["id"]]["file"]
				var value = idata.sell_value
				if idata.generate_worth:
					var t = 1.0 + (0.25 * idata.tier)
					var w = idata.loot_weight
					value = pow(5 * t, 2.5 - w)
						
					if w < 0.4: value *= 1.1
					if w < 0.15: value *= 1.25
					if w < 0.05: value *= 1.5
					
				var worth = ceil(value * mult * PlayerData.QUALITY_DATA[item["quality"]].worth)
				stockvalue += worth
				var alive = Globals.item_data[item["id"]]["file"].alive
				if item["quality"] == 0 and alive: indqual += 5
				elif item["quality"] == 1 and alive: indqual += 10
				elif item["quality"] == 2 and alive: indqual += 15
				elif item["quality"] == 3 and alive: indqual += 25
				elif item["quality"] == 4 and alive: indqual += 40
				elif item["quality"] == 5 and alive: indqual += 50
				elif not alive and file.item_name != "Coin Bag":
					var trashdeduction = item["quality"]
					trashdeduction *= 10
					if not trashdeduction == 0: indqual -= randi() % trashdeduction + 1
					else: indqual -= randi() % 10 + 1
				var rarity = Globals.item_data[item["id"]]["file"].rare
				if rarity: indqual += 100
				var difficulty = Globals.item_data[item["id"]]["file"].catch_difficulty
				if difficulty >= 2.5:
					difficulty -= 2.5
					difficulty *= 10
					indqual += difficulty
				randomize()
				var keep = randi() % 20 + 1
				var lost = false
				if keep <= 5: lost = true
				elif keep > 5 and keep <= 17: confirmeditems += 1
				elif keep > 17:
					randomize()
					var keep2 = randi() % 20 + 1
					if keep2 > 4 and keep2 <=6:
						confirmeditems += 2
					elif keep2 == 7:
						confirmeditems += 7
					elif keep2 > 1 and keep2 < 4:
						confirmeditems += 3
					elif keep2 > 11 and keep2 <= 14:
						confirmeditems += 3
					elif keep2 > 17:
						confirmeditems += 4
				if indqual > 40 and keep >= 13 and not lost and globalpity < clamp((temprolltotal * 250),0,50000):
					randomize()
					var glhf = randi() % 20 + 1
					if glhf <= 3:
						var wedidit = randi() % 4
						confirmeditems += wedidit
				elif indqual > 40 and keep >= 13 and not lost:
					randomize()
					var glhf = randi() % 20 + 1
					if glhf <= 3:
						var wedidless = randi() % 3
						confirmeditems += wedidless
				
				qualityweight += indqual
				actualitems +=1
				# end of item parsing
				
			if not getlucky and actualitems > 12:
				randomize()
				var tryagain = randi() % 7 + 1
				if tryagain == 7: getlucky = true
				
			var pitied = false
			var doublepity = false
			var pitypoints = confirmeditems
			if pitysystem != []: for pit in pitysystem:
				if pit[0] == whogets and pit[1] >= 2:
					pitied = true
				if pit[0] == whogets and pit[1] >= 4:
					doublepity = true
					pitypoints *= 2
			var gpc1 = -15000 + clamp((temprolltotal * 250),0,50000)
			if globalpity < gpc1:
				randomize()
				var goobly = randi() % 3 + 1
				if goobly == 1: pitypoints*=2
			var averageweight = 0
			if actualitems > 0: averageweight = int(round(qualityweight/actualitems))
			else:
				continue
			
			var returnitems = []
			
			var current_tiniest
			var current_largest = 0.0
			var firstsized = false
			
			randomize()
			var loss = randi() % 100 + 1
			if tootsies:
				loss = 95
				getlucky = false
			var gpc2 = 50000 + clamp((temprolltotal * 250),0,50000)
			if loss >= 90 and not getlucky: confirmeditems = 0
			elif loss >= 67 and not getlucky and globalpity > gpc2: confirmeditems = 0
			if loss > 3 and getlucky:
				loss = randi() % 90 + 1
			var singlefish = false
			var singlefishtype
			var rolls = []
			
			# a whole slew of checks that would constitute a jackpot
			var check1 = loss <= 1 and averageweight >= 10
			var check2 = loss <= 1 and averageweight >= 20
			var check3 = loss <= 1 and averageweight >= 30
			var check4 = loss <= 2 and averageweight >= 40
			var check5 = loss <= 3 and averageweight >= 50
			var check6 = loss <= 4 and averageweight >= 60
			var check7 = loss <= 5 and averageweight >= 70
			var gpc3 = -10000 + clamp((temprolltotal * 250),0,50000)
			var gpc4 = -25000 + clamp((temprolltotal * 250),0,50000)
			var gpc5 = clamp((temprolltotal * 250),0,50000)
			var gpc6 = 5000 + clamp((temprolltotal * 250),0,50000)
			var gpc7 = -5000 + clamp((temprolltotal * 250),0,50000)
			var gpc8 = 25000 + clamp((temprolltotal * 250),0,50000)
			var gpc9 = -15000 + clamp((temprolltotal * 250),0,50000)
			var gpc10 = -50000 + clamp((temprolltotal * 250),0,50000)
			var check8 = loss <= 4 and averageweight >= 10 and globalpity < gpc3
			var check9 = loss <= 7 and averageweight >= 15 and globalpity < gpc4
			var check10 = loss == 51 and averageweight >= 20 and globalpity < gpc5
			var check11 = loss > 45 and loss <= 47 and averageweight >= 25 and globalpity < gpc4
			var check12 = loss == 75 and averageweight >= 30 and actualitems > 6 and globalpity < gpc6
			var check13 = loss > 75 and loss <= 77 and averageweight >= 35 and actualitems > 12 and globalpity < gpc7
			var checklist = check1 or check2 or check3 or check4 or check5 or check6 or check7 or check8 or check9 or check10 or check11 or check12 or check13
			var fullcheck = globalpity < gpc6 and averageweight >= 0 and checklist
			#jackpot check
			if fullcheck:
				singlefish = true
				var counter = 7
				if pitied: counter +=6
				if globalpity < gpc4: counter += 8
				for i in counter:
					randomize()
					var fishtype = ""
					var fisht = randi() % 100 + 1
					if fisht <= 17: fishtype = "lake"
					elif fisht > 17 and fisht <= 35: fishtype = "ocean"
					elif fisht > 35 and fisht <= 75: fishtype = "rain"
					elif fisht: fishtype = "alien"
					singlefishtype = Globals._roll_loot_table(fishtype, 2)
					rolls.append([singlefishtype,"egg"])
				var chosen = rolls[0]
				for roll in rolls:
					var old_tier = Globals.item_data[chosen[0]]["file"].tier
					var new_tier = Globals.item_data[roll[0]]["file"].tier
					if new_tier > old_tier:
						chosen = roll
					var old_diff = Globals.item_data[chosen[0]]["file"].catch_difficulty
					var new_diff = Globals.item_data[roll[0]]["file"].catch_difficulty
					if new_diff > old_diff:
						chosen = roll
					
					
					var new_rare = Globals.item_data[roll[0]]["file"].rare
					if new_rare:
						chosen = roll
				singlefishtype = chosen[0]
				confirmeditems += 7
			var megajackpot = false
			if singlefish:
				randomize()
				var truth = randi() % 100 + 1
				if globalpity < gpc10: truth -= 6
				if truth <= 7:
					randomize()
					var megajack = randi() % 5 + 3
					confirmeditems *= megajack
					megajackpot = true
			confirmeditems = clamp(confirmeditems,0,200)
			
			var letterindex = 1
			var returnindex = 0
			#generating rewards
			for e in confirmeditems:
				if returnindex == 20:
					returnindex = 0
					letterindex += 1
				randomize()
				rolls = []
				var roll = Globals._roll_loot_table("lake", 2)
				var fish_roll = Globals._roll_loot_table("lake", 2)
				if not singlefish:
					var hmany = randi() % 6 + 5
					var addable = averageweight
					var qboost = 0
					for q in 7:
						if addable >= 10:
							addable -= 10
							hmany += (randi() % 2 + 1)
							qboost += (randi() % 2 + 1)
					if averageweight < 0: hmany = 1
					if pitied:
						var adder = randi() % 3 + 1
						hmany += adder
						if doublepity: hmany += adder
					if globalpity < gpc7: hmany += 3
					if globalpity < gpc3: hmany += 5
					if actualitems > 6:
						hmany += 2
						qboost += 2
					if actualitems > 12:
						hmany += 1
						qboost += 3
					for i in hmany:
						var fishtype = ""
						var fisht = randi() % 100 + 1
						if getlucky and fisht <= 15: fisht = randi() % 100 + 1
						if fisht <= 1: fishtype = "void"
						elif fisht > 1 and fisht <= 15: fishtype = "water_trash" # diamonds/weed only?
						elif fisht > 1 and fisht <= 33 and globalpity > gpc8: fishtype = "water_trash"
						elif fisht > 15 and fisht <= 50: fishtype = "lake"
						elif fisht > 50 and fisht <= 85: fishtype = "ocean"
						elif fisht > 85 and fisht <= 99: fishtype = "rain"
						else: fishtype = "alien"
						if pitied and fishtype == "water_trash" and pitypoints > 0:
							var which = randi() % 3 + 1
							if which == 1:
								fishtype = "alien"
								pitypoints -= 1
						
						roll = Globals._roll_loot_table(fishtype, 2)
						rolls.append([roll,"egg"])
					var chosen = rolls[0]
					qboost += 15
					for roller in rolls:
						randomize()
						var chunky = randi() % qboost + 1
						var funky = randi() % qboost + 1
						var old_tier = Globals.item_data[chosen[0]]["file"].tier
						var new_tier = Globals.item_data[roller[0]]["file"].tier
						if chunky >= 12 and new_tier > old_tier and averageweight > 5:
							chosen = roller
						elif new_tier > old_tier and pitied and pitypoints > 0:
							randomize()
							var dowe = randi() % 3 + 1
							if dowe == 1:
								chosen = roller
								pitypoints -= 1
						elif chunky <= 2 and old_tier > new_tier:
							chosen=roller
						if old_tier > new_tier and averageweight < 0:
							chosen = roller
							
						var old_diff = Globals.item_data[chosen[0]]["file"].catch_difficulty
						var new_diff = Globals.item_data[roller[0]]["file"].catch_difficulty
						if new_diff > old_diff and pitied and pitypoints > 1:
							randomize()
							var yeahprob = randi() % 3 + 1
							if yeahprob == 1:
								chosen = roller
								pitypoints -= 2
						elif new_diff > old_diff and funky > 13 and averageweight > 0:
							chosen = roller
						elif old_diff > new_diff and funky >= 3 and funky < 5 and not pitied and averageweight > 0:
							chosen = roller
						elif old_diff > new_diff and funky >= 4 and funky < 5 and pitied and averageweight > 0:
							chosen = roller
						if old_diff > new_diff and averageweight < 0:
							chosen = roller
						var new_rare = Globals.item_data[roller[0]]["file"].rare
						if new_rare and funky < 3 and averageweight > 25:
							chosen = roller
						elif new_rare and pitied and pitypoints > 0:
							randomize()
							var wedo = randi() % 3 + 1
							if wedo == 1:
								chosen = roller
								pitypoints -= 1
					fish_roll = chosen[0]
				else:
					fish_roll = singlefishtype
				
				# overrides (iykyk)
				if thritis: fish_roll = "fish_ocean_lobster"
				
				# size determination
				var sizes = []
				var sizeq = randi() % 6 + 2
				if pitied:
					var tongue = randi() % 3 + 1
					sizeq += tongue
					if doublepity:
						sizeq += tongue
				var size = Globals._roll_item_size(fish_roll)
				var finalsize = Globals._roll_item_size(fish_roll)
				for sizeo in sizeq:
					size = Globals._roll_item_size(fish_roll)
					sizes.append([size,"egg"])
				size = sizes[0]
				for sizing in sizes:
					randomize()
					var sizerand = randi() % 15 + 1
					if sizerand < 3 and not singlefish and not pitied:
						if sizing[0] < size[0]:
							size = sizing
					elif sizerand < 2 and not singlefish and pitied:
						if sizing[0] < size[0]:
							size = sizing
					elif sizerand >=3 and sizerand < 7 and not singlefish:
						pass
					else:
						if size[0] < sizing[0] or singlefish:
							size = sizing
						elif size[0] < sizing[0] and pitied and pitypoints > 0:
							randomize()
							var charlie = randi() % 3 + 1
							if charlie == 1:
								size = sizing
								pitypoints -= 1
				finalsize = size[0]
				if firstsized == false:
					current_tiniest = finalsize
					current_largest = finalsize
				else:
					if finalsize > current_largest: current_largest = finalsize
					if finalsize < current_tiniest: current_tiniest = finalsize
				var fishdata = Globals.item_data[fish_roll]["file"]
				var quality = PlayerData.ITEM_QUALITIES.NORMAL
				randomize()
				var qualrand = randi() % 111 - 50
				qualrand += averageweight
				qualrand = int(clamp(qualrand,1,100))
				if qualrand < 65 and getlucky:
					qualrand = randi() % 111 - 25
					qualrand += averageweight
					qualrand = int(clamp(qualrand,1,100))
				if qualrand < 65 and pitied and pitypoints > 0:
					randomize()
					var tubes = randi() % 2 + 0
					if tubes == 1:
						qualrand = randi() % 137 - 25
						qualrand += averageweight
						qualrand = int(clamp(qualrand,1,100))
						pitypoints -= 1
				if qualrand < 65 and doublepity:
					randomize()
					qualrand = randi() % 137 - 25
					qualrand += averageweight
					qualrand = int(clamp(qualrand,1,100))
				if qualrand >= 25 and qualrand < 50:
					quality = PlayerData.ITEM_QUALITIES.SHINING
				elif qualrand >= 50 and qualrand < 75:
					quality = PlayerData.ITEM_QUALITIES.GLISTENING
				elif qualrand >= 75 and qualrand < 85:
					quality = PlayerData.ITEM_QUALITIES.OPULENT
				elif qualrand >= 85 and qualrand < 95:
					quality = PlayerData.ITEM_QUALITIES.RADIANT
				elif qualrand >= 95:
					quality = PlayerData.ITEM_QUALITIES.ALPHA
				if singlefish: quality = PlayerData.ITEM_QUALITIES.ALPHA
				randomize()
				var refrand = randi()
				var entry = {"id": fish_roll, "size": finalsize, "ref": refrand, "quality": quality,"tags": []}
					
				entry = PlayerData._validate_item_safety(entry)
				
				# evaluating the price (again, price structure from PlayerData, had to adapt since non-inventory)
				var file = Globals.item_data[entry["id"]]["file"]
				if file.unselectable or not file.can_be_sold: continue
				var size_prefix = {
					0.1: 1.75, 
					0.25: 0.6, 
					0.5: 0.8, 
					1.0: 1.0, 
					1.5: 1.5, 
					2.0: 2.5, 
					3.0: 4.25, 
				}
			
				var average = Globals.item_data[entry["id"]]["file"].average_size
				var calc = entry["size"] / average
				var mult = 1.0
					
				for p in size_prefix.keys():
					if p > calc: break
					mult = size_prefix[p]
					
				var idata = Globals.item_data[entry["id"]]["file"]
				var value = idata.sell_value
				if idata.generate_worth:
					var t = 1.0 + (0.25 * idata.tier)
					var w = idata.loot_weight
					value = pow(5 * t, 2.5 - w)
						
					if w < 0.4: value *= 1.1
					if w < 0.15: value *= 1.25
					if w < 0.05: value *= 1.5
					
				var worth = ceil(value * mult * PlayerData.QUALITY_DATA[entry["quality"]].worth)
				# give a fish a name and they'll wear a nametag for a day. teach a fish to name and we're all fucked
				var cname = ""
				if singlefish: cname = "JACKPOT"
				else:
					randomize()
					var suit = randi() % 4 + 1
					match suit:
						1: cname = "hearts"
						2: cname = "clubs"
						3: cname = "diamonds"
						4: cname = "spades"
					if worth < 1000:
						randomize()
						var card = randi() % 9 + 2
						match card:
							2: cname = "two of " + cname
							3: cname = "three of " + cname
							4: cname = "four of " + cname
							5: cname = "five of " + cname
							6: cname = "six of " + cname
							7: cname = "seven of " + cname
							8: cname = "eight of " + cname
							9: cname = "nine of " + cname
							10: cname = "ten of " + cname
					elif worth >= 1000:
						randomize()
						var card = randi() % 4 + 1
						match card:
							1: cname = "jack of " + cname
							2: cname = "queen of " + cname
							3: cname = "king of " + cname
							4: cname = "ace of " + cname
						
				var entry_final = {"id": fish_roll, "size": finalsize, "ref": refrand, "quality": quality, "custom_name":cname,"tags": []}
				entry_final = PlayerData._validate_item_safety(entry_final)
				returnitems.append(entry_final)
				returnindex += 1
				
				aftermarket += worth
				
			var truewin = aftermarket
			aftermarket = aftermarket - stockvalue
			
			var realconfirmeditems = confirmeditems
			var fishnet = confirmeditems - actualitems
			var lbstring = ""
			
			# add roll data to the leaderboard
			var afound = false
			if gambaleaderboard != []:
				for gamblur in gambaleaderboard:
					if gamblur["id"] == whogets:
						afound = true
						var dolla_in = gamblur["dolla_in"]
						var dolla_out = gamblur["dolla_out"]
						var dolla_net = gamblur["dolla_net"]
						var jackpots = gamblur["jackpots"]
						var highest_jackpot = 0
						if gamblur.has("highest_jackpot"): highest_jackpot = gamblur["highest_jackpot"]
						var fish_in = gamblur["fish_in"]
						var fish_out = gamblur["fish_out"]
						var fish_net = gamblur["fish_net"]
						var tot_dolla_in = gamblur["tot_dolla_in"]
						var tot_dolla_out = gamblur["tot_dolla_out"]
						var tot_dolla_net = gamblur["tot_dolla_net"]
						var tot_jackpots = gamblur["tot_jackpots"]
						var tot_fish_in = gamblur["tot_fish_in"]
						var tot_fish_out = gamblur["tot_fish_out"]
						var tot_fish_net = gamblur["tot_fish_net"]
						var tiniest_yet = gamblur["tiniest_yet"]
						var lorgest_yet = gamblur["lorgest_yet"]
						gambaleaderboard.erase(gamblur)
						
						dolla_in += stockvalue
						dolla_out += truewin
						dolla_net += aftermarket
						fish_in += actualitems
						fish_out += realconfirmeditems
						fish_net += fishnet
						tot_dolla_in += stockvalue
						tot_dolla_out += truewin
						tot_dolla_net += aftermarket
						tot_fish_in += actualitems
						tot_fish_out += realconfirmeditems
						tot_fish_net += fishnet
						if current_tiniest < tiniest_yet: tiniest_yet = current_tiniest
						elif tiniest_yet == 0: tiniest_yet = current_tiniest
						if current_largest > lorgest_yet: lorgest_yet = current_largest
						if singlefish:
							jackpots += 1
							tot_jackpots +=1
							if truewin > highest_jackpot: highest_jackpot = truewin
							
						gambaleaderboard.append({"id":whogets,"dolla_in":dolla_in,"dolla_out":dolla_out,"dolla_net":dolla_net,"jackpots":jackpots,"highest_jackpot":highest_jackpot,"fish_in":fish_in,"fish_out":fish_out,"fish_net":fish_net,"tot_dolla_in":tot_dolla_in,"tot_dolla_out":tot_dolla_out,"tot_dolla_net":tot_dolla_net,"tot_jackpots":tot_jackpots,"tot_fish_in":tot_fish_in,"tot_fish_out":tot_fish_out,"tot_fish_net":tot_fish_net,"tiniest_yet":tiniest_yet,"lorgest_yet":lorgest_yet})
			if afound == false:
				var dolla_in = stockvalue
				var dolla_out = truewin
				var dolla_net = aftermarket
				var fish_in = actualitems
				var fish_out = realconfirmeditems
				var fish_net = fishnet
				var tot_dolla_in = stockvalue
				var tot_dolla_out = truewin
				var tot_dolla_net = aftermarket
				var tot_fish_in = actualitems
				var tot_fish_out = realconfirmeditems
				var tot_fish_net = fishnet
				var tiniest_yet = current_tiniest
				var lorgest_yet = current_largest
				var jackpots = 0
				var tot_jackpots = 0
				var highest_jackpot = 0
				if singlefish:
					jackpots += 1
					tot_jackpots += 1
					highest_jackpot = truewin
				gambaleaderboard.append({"id":whogets,"dolla_in":dolla_in,"dolla_out":dolla_out,"dolla_net":dolla_net,"jackpots":jackpots,"highest_jackpot":highest_jackpot,"fish_in":fish_in,"fish_out":fish_out,"fish_net":fish_net,"tot_dolla_in":tot_dolla_in,"tot_dolla_out":tot_dolla_out,"tot_dolla_net":tot_dolla_net,"tot_jackpots":tot_jackpots,"tot_fish_in":tot_fish_in,"tot_fish_out":tot_fish_out,"tot_fish_net":tot_fish_net,"tiniest_yet":tiniest_yet,"lorgest_yet":lorgest_yet})
			
			# sends stats if requested
			
			if testes:
				#lol like balls
				_gamba_stats(whogets)
				
			# build the string, adjust pity systems
			var bstring = " gambled $" + str(stockvalue) + " worth of fish and"
			if confirmeditems == 0: bstring = " gambled $" + str(stockvalue) + " worth of fish and lost it all."
			var checkers = clamp(temprolltotal * 250,0,50000)
			if globalpity > checkers:
				globalpity = int(globalpity * 0.9)
				var tinycheckers = temprolltotal * 20
				tinycheckers = clamp(tinycheckers,0,2000)
				globalpity -= tinycheckers
			globalpity += aftermarket
			var stopper = false
			if aftermarket > 0 and confirmeditems != 0: # gain
				var found = false
				if pitysystem != []: for pit in pitysystem:
					if pit[0] == whogets:
						var count = pit[1]
						count = 0
						pit[1] = count
						found = true
				if found == false:
					pitysystem.append([whogets,0])
				if singlefish:
					if megajackpot:
						bstring = bstring + " hit the MEGA jackpot, gaining $" + str(aftermarket) + " in value."
					else:
						bstring = bstring + " hit the jackpot, gaining $" + str(aftermarket) + " in value."
				else:
					bstring = bstring + " won $" + str(truewin) + " (+$" + str(aftermarket)
			elif aftermarket < 0 and confirmeditems != 0: # loss
				var found = false
				if pitysystem != []: for pit in pitysystem:
					if pit[0] == whogets:
						var count = pit[1]
						count += 1
						pit[1] = count
						found = true
				if found == false:
					pitysystem.append([whogets,1])
				bstring = bstring + " won $" + str(truewin) + " (-$" + str(-aftermarket)
			elif aftermarket == 0 and confirmeditems != 0:
				bstring = bstring + " won exactly $" + str(truewin) + " back. how'd you manage that??"
				stopper = true
			if confirmeditems != 0:
				if fishnet > 0 and not singlefish:
					bstring = bstring + ", +" + str(fishnet) + " fish)"
				elif fishnet < 0 and not singlefish:
					bstring = bstring + ", " + str(fishnet) + " fish)"
				elif fishnet == 0 and not stopper and not singlefish:
					bstring = bstring + ")"
			var mstring = bstring
			# add the fishexchange host's name to mailouts about pity, eliminate some confusion
			var hostname = ""
			for member in Network.LOBBY_MEMBERS:
				if member["steam_id"] == Network.STEAM_ID:
					hostname = str(member["steam_name"])
			bstring = "you" + bstring + "\nyour submitted fish score was " + str(averageweight) + " points."
			if globalpity < gpc4: bstring = bstring + "\n"+str(hostname)+"'s lobby jackpot pity is currently at its peak.\n-fish exchange"
			elif globalpity < gpc9: bstring = bstring + "\n"+str(hostname)+"'s lobby jackpot pity is currently very high.\n-fish exchange"
			elif globalpity < gpc3: bstring = bstring + "\n"+str(hostname)+"'s lobby jackpot pity is currently high.\n-fish exchange"
			elif globalpity < gpc7: bstring = bstring + "\n"+str(hostname)+"'s lobby jackpot pity is currently rising.\n-fish exchange"
			else: bstring = bstring + "\n-fish exchange"
			
			
			
			temprolltotal += 1
			temprollog.append([str(temprolltotal),str(aftermarket),str(averageweight),str(stockvalue),str(truewin)])
		
			# mail out the letters
			
			var mfound = false
			var lettertuna = 1
			var lettersent = false
			for final in letterindex:
				var gstring = ""
				if letterindex > 1: gstring = " (" + str(lettertuna) + " of " + str(letterindex) + ")"
				var dstring = bstring + gstring
				lettertuna += 1
				randomize()
				var letter_id = randi()
				var ref = randi()
				var indek = 0
				var selectitems = []
				for i in 20:
					if returnitems != []:
						selectitems.append(returnitems.pop_back())
					else: break
				var returndata = {"letter_id": letter_id, "header": "gamba", "closing": "From, ", "body": dstring, "items": selectitems, "to": str(whogets), "from": str(Network.STEAM_ID), "user": str(Network.STEAM_USERNAME)}
				var text = ""
			
					
			
				for member in Network.LOBBY_MEMBERS:
					if int(whogets) == int(member["steam_id"]):
						if not mfound: mstring = str(member["steam_name"]) + mstring
						mfound = true
						
						if member["steam_id"] != Network.STEAM_ID:
							Network._send_P2P_Packet({"type": "letter_recieved", "data": returndata, "to": str(whogets)}, str(whogets), 2, Network.CHANNELS.GAME_STATE)
						else :
							PlayerData._recieved_letter(returndata)
						lettersent = true
						yield (get_tree().create_timer(0.1), "timeout")
			
			if lettersent:
				PlayerData._send_notification(mstring)
				PlayerData.inbound_mail.erase(letter)
				PlayerData.emit_signal("_letter_update")
			else:
				PlayerData._send_notification("error? letter not sent.")
		else:
			PlayerData._send_notification("letter items are not an array, perhaps!")
		return
