I have known about this vulnerability for years, but recently I've figured out that this exploit is not very well known. In this blog post I'll show you how you can do it and what you can do to protect yourself from this.

I've been able to use this exploit successfully going all the way back to Windows XP, but weirdly enough it still exists in Windows 10 today. I've used it multiple times to get back into PC's of my own or friends/family when they forgot their passwords. There were even couple times I even got to use it at work when I was in IT, specifically when a PC would fall off the domain from not being used and the local admin password was not known for that PC.

### So how does this work? 
The basics of the vulnerability are that you can replace the Sticky Keys executable file on the disk with a copy of Command Prompt, this gives you access to an admin level prompt on the login screen by pressing 'Shift' 5 times. Don't worry, I'll have plenty of screenshots to make this easy to follow along as well.

I'm starting off with a fresh install of Windows 10, updated to the latest patch at the time of writing (20H2 19042.964). After installing the drivers and updates I finally add a password.

Time to reboot, but oh no! It looks like I may have forgotten my password and left myself a very unhelpful hint for it.
![Locked Windows](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/LockedWindows.png "Locked Windows")

The easiest way for us to get back in is to use this vulnerability, to do this all we need a Windows Installer USB. So lets set that up.

Go to Microsoft's website and download the tool to create a USB, download link available [here](https://www.microsoft.com/en-us/software-download/windows10). Be warned that this WILL erase the entire USB you choose to use, but once this is done you should have a bootable Windows 10 installer on your USB.

Once that is done, you can plug it in and reboot the computer. You'll need to get into your BIOS now, which is different on every PC so I can't help you much with that, but the instructions are usually fairly easy to find online.

In the BIOS you need to boot from the USB we just created, again this is different for every PC so [DuckDuckGo](https://duckduckgo.com) is your friend here!

Once you have the USB booted, you should be greeted with the Windows installer. On this you can click 'Next' on the language selection, then on the next screen there is a tiny button in the bottom left that says 'Repair your computer', click that. 

![Select Language](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/SelectLanguage.png "Select Language")
![Repair Your Computer](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/RepairYourComputer.png "Repair Your Computer")

Next you need to click on 'Troubleshoot' and finally on 'Command Prompt'.

![Choose an Option 1](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/ChooseAnOption1.png "Choose an Option 1")
![Advanced Options](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/AdvancedOptions.png "Advanced  Options")


Next you'll need to figure out which drive to use, start by trying C, then D, then E, etc. Until you find one that has the 'Windows' directory on it. You usually won't have to go any higher than D, but if you have a lot of partitions or drives in your PC this could take a few more tries.

You can do this with the <code>&lt;drive letter&gt;:</code> and <code>dir</code> commands like so:

![Find Windows](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/FindWindows.png "Find Windows")

Once you've found your Windows directory, you can change directory to <code>Windows\System32</code>. Here we need to do 2 things, first we need to make a backup if our <code>sethc.exe</code> (Sticky Keys). Then we need to replace the original <code>sethc.exe</code> with a copy of <code>cmd.exe</code> (Command Prompt).

![Backup and Replace Sticky Keys](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/BackupAndReplaceStickyKeys.png "Backup and Replace Sticky Keys")

Once that is done we can reboot and get back into Windows 10. Simply exit out of Command Prompt and select 'Continue'.

![Choose an Option 2](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/ChooseAnOption2.png "Choose an Option 2")

Once you reboot and your back on the login screen, press 'Shift' 5 times and you should be greeted with an administrator level Command Prompt.

![Locked Windows CMD](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/LockedWindowsCmd.png "Locked Windows CMD")

From here you can either enable the Administrator account or reset your own password. In the screenshot below I first list the user accounts, then enable the Administrator account, then reset the password of my own account to 'YourPassword'. 

![Enable Administrator and Reset Password](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/EnableAdministratorAndResetPassword.png "Enable Administrator and Reset Password")

The first and last command are the most important here, first you need to know your username, and if yours has a space in it like mine does you will need to put it in quotes for the last command. The 'YourPassword' I add there is the password you want to set for your account, this can be whatever you'd like. Once you set the password you can login to your account now! But wait, we are not done, still a couple cleanup items to do. Once you know your password and you're back into your account you'll need to undo the change to your sticky keys. We can do this a few different ways, but the easiest in my opinion is to just let Windows do it.

Open up a Command Prompt as admin and delete the <code>sethc.exe</code> we just created a few minutes ago. Next you'll notice that we can't simply rename our <code>sethc.exe.backup</code> we created back to <code>sethc.exe</code>. But no problem, we can just use the System File Checker to do this for us. 

![Cleanup](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/Cleanup.png "Cleanup")

Once this is complete you should be able to press shift 5 times again and Sticky Keys will re-open, so now we are done!

### How can you protect yourself from this?
The easiest way to protect yourself from this is by disabling Sticky Keys, that will prevent Windows from opening Command Prompt on the lock screen if someone were to try doing this. To disable sticky keys simply search for 'Sticky Keys' in the search bar. Select 'Allow the shortcut key to start Sticky Keys', from here Windows should take you into the Settings and highlight the checkbox you need to disable. Simply uncheck it and you should be protected now.

![Search Sticky Keys](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/SearchStickyKeys.png "Search Sticky Keys")
![Disable Sticky Keys](/images/blog/2021-05-10-bypass-windows-login-with-sticky-keys/DisableStickyKeys.png "Disable Sticky Keys")

However I know that a lot of people rely on Sticky Keys for accessability reasons, so that isn't always the best solution. For those that use Sticky Keys I don't have a great solution for you, but if you have Windows 10 Pro you can enable BitLocker encryption for your hard drive which will also protect you from this. Sadly this solution has a major downside since you'll need to enter a password every time you start your computer, and for most people this is not possible since the majority of PC's you can buy will come with Windows 10 Home edition which doesn't have BitLocker available.

### What can Microsoft do to fix this?
The easiest thing for them to do would be to change Windows so that it uses System File Checker to do an integrity check on the Sticky Keys executable when launched from the lock screen. But sadly I'm not sure that they will ever do that, since this vulnerability has been around for so many years now and they have seemingly done nothing to fix it.