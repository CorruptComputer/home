---
layout: post
title:  "Digital Ocean and a Discord Bot"
date:   2017-03-31 10:30:00
lastmod: 2021-03-07 12:00:00
image: /images/blog/digital ocean discord bot.png
---

If anyone has ever used [NadekoBot](https://github.com/Kwoth/NadekoBot), you know that its great. But you know what could make it even better? Being 24/7.

Ok so its fairly simple if you follow the [guide](http://nadekobot.readthedocs.io/en/latest/guides/Linux%20Guide/), but I quickly ran into a problem. 
When I would end the ssh session, the bot would shutdown. I tried a few things, the first being: make a new user, run the bot there, then switch users leaving the old one logged in.
However that didn't really work either as all users were logged out when I ended the ssh session. So next I moved on to try `tmux`, and to my surprise it actually worked! 

1) So to get started you're just gonna type `tmux new -s nadeko`  
2) After that you just start Nadeko normally with `bash linuxAIO.sh`  
3) Once the bot is running and configured as you would like, type `ctrl+b` then `d`. This detaches the process from the current user and allows it to keep running when you logout.

If you want to reattach your bot you just type `tmux attach -t nadeko` and you'll be right back to the bot. 
Personally I have mine setup to reboot and update every night at midnight, however if you wanted to do that manually this is an easy way to.

EDIT: Instructions have also been added to the docs [here](https://nadekobot.readthedocs.io/en/latest/guides/Linux%20Guide/#running-nadekobot).