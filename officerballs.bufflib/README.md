# buff lib  
adds 24 buffs and 5 boons to enhance gameplay. this does not function standalone but is for myself and other modders if they wish to utilize its functions.  
now includes new setting: RandomBuffsFromFishing - enable to have a chance to get random buffs/boons while fishing.  
  
for support/conflicts please create a GitHub issue.  
  
## buffs and boons  
buff_salty - treat nearby water as salt  
buff_fresh - treat nearby water as fresh  
buff_rain - chance to catch rain fish whether in rain or not  
buff_void - chance to catch void fish anywhere  
buff_alien - chance to catch ufo fish anywhere  
  
buff_small - acts as a small lure  
buff_large - acts as a large lure  
buff_quality - acts as a sparkling lure  
buff_rarity - acts as a gold lure  
  
buff_timestretch - the red bar creeps at 1/2 speed while fishing  
buff_cantlosefish - the red bar doesn't creep at all while fishing  
buff_lucky - get more bite chance% per tick while fishing  
buff_fastbite - fishing tickrate is higher  
buff_double - acts as a double lure  
buff_haste - acts as a quick jig (faster reeling)  
buff_clickreduce - lowers fish health by 50%  
buff_clickmultiply - makes clicks count 2x  
buff_gatereduce - lowers the number of gates by 50%  
buff_valuelift - temporarily makes your fish sell for 200%  
buff_efficiency - acts as an efficient lure  
buff_speedbuddies - makes fishing buddies tick twice as fast  
buff_doublebuddies - gives fishing buddies a chance to yield doubles  
buff_protection - protects you from boons  
buff_gamblefisher - gives a chance to get scratch offs while fishing  
  
boon_redcreep - the red bar creeps twice as fast while fishing  
boon_weakening - your clicks are half as efficient while fishing  
boon_slowbite - fish will bite roughly twice as slow  
boon_trash - acts as magnet lure  
boon_slowness - slows you down!  
  
## to use these in your mods  
you're welcome to include this as a dependency for your own mods.  
when referencing the local player, utilize the following commands (using local_player as an example reference):  
  
local_player._add_buff("buff_salty", 60, 3)  
  
this would add the "salty" buff at tier 3 for one minute.  
all buffs except 'cantlosefish' and 'protection' have a tier from 1 to 5.  
  
if you wish to apply a boon, utilize _add_boon instead.  
_wipe_buff("buff_example") and _wipe_boon("boon_example") will remove buffs.  
additionally: _random_buff_or_boon(), _wipe_all_buffs() and _wipe_all_boons() are usable.  
  
want to support my work? consider buying me a donut at my ko-fi!  
[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/S6S519BLBL)
