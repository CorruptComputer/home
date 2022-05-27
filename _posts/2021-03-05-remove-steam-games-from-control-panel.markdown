---
layout: post
title:  "Removing Steam Games from Control Panel and Apps list on Windows"
excerpt: "It might just be me, but I find these to be absolutely useless and actually get in the way of actually finding the apps that I want to remove."
date:   2021-03-05 23:00:00
lastmod: 2022-05-26 23:00:00
---

It might just be me, but I find these to be absolutely useless and actually get in the way of actually finding the apps that I want to remove. 
Not only that but when you try to uninstall these from here, it does nothing but launch Steam so why have them here in the first place?

If you're like me and have almost 60 games installed it can become almost impossible to find what you need.
Thankfully though there is a simple way to remove these from the list using PowerShell.

You can run the following PowerShell script (as Admin):
<pre><code class="language-powershell">$counter = 0;

foreach ($steamGame in (reg query HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall | findstr /C:"Steam App")) {
    reg delete $steamGame /f;
    $counter += 1;
}

foreach ($steamGame in (reg query HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall | findstr /C:"Steam App")) {
    reg delete $steamGame /f;
    $counter += 1;
}

if ($counter -NE 0) {
    Write-Host ("Removed " + $counter + " games from Control Panel and Apps list.");
} else {
    Write-Host ("No games found.");
}</code></pre>

The output should look something like this for you:
![Steam Games Image 1](/images/blog/2021-03-05-remove-steam-games-from-control-panel/SteamGames1.png "Steam Games Image 1"){:.blogImageInline}

<hr />

As an extension to this you could add the script as a Scheduled Task so that you don't need to manually run it every time you install a game. 

Open Task Scheduler and Create a new Task:
![Steam Games Image 2](/images/blog/2021-03-05-remove-steam-games-from-control-panel/SteamGames2.png "Steam Games Image 2"){:.blogImageInline}

General Settings:
![Steam Games Image 3](/images/blog/2021-03-05-remove-steam-games-from-control-panel/SteamGames3.png "Steam Games Image 3"){:.blogImageInline}

Create a new Trigger to have it run when you'd like. Personally I have it set to run whenever I log on:
![Steam Games Image 4](/images/blog/2021-03-05-remove-steam-games-from-control-panel/SteamGames4.png "Steam Games Image 4"){:.blogImageInline}

Next create the action to run:
- Program/Script: <inline-code>C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe</inline-code>
- Add arguments: <inline-code>-ExecutionPolicy Bypass -File "C:\[path to your file]\Remove-Steam-Games-From-Control-Panel.ps1"</inline-code>
- Start in: <inline-code>C:\[path to your file]</inline-code>
![Steam Games Image 5](/images/blog/2021-03-05-remove-steam-games-from-control-panel/SteamGames5.png "Steam Games Image 5"){:.blogImageInline}

Aaaaaaand you're done!

Now your Control Panel and Apps list will be a bit cleaner and easier to find stuff in. 
If you want you can also check out my other guide on [how to make your install of Windows a bit more privacy friendly]({{ site.url }}/2019/03/14/privacy-and-windows-10) than it would be out of the box.