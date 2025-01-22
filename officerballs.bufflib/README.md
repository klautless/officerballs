# buff lib  
adds 22 buffs and 5 boons to enhance gameplay. this does not function standalone but is for myself and other modders if they wish to utilize its functions. to test them out, use [red chalk](https://thunderstore.io/c/webfishing/p/officer_balls/redchalk/).  
  
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
when referencing the local player, local_player.ob_buffs["buff_salty"] += 60 would increase the salty buff by one minute.  
boons utilize .ob_boons["boon_redcreep"] (example) instead.  
  

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/S6S519BLBL)
