---
layout: post
title:  "Privacy and Windows 10"
excerpt: "Windows 10 has been a nightmare from a privacy perspective, however it doesn't have to be."
date:   2019-03-14 14:00:00
lastmod: 2022-05-26 23:00:00
---

Windows 10 has been a nightmare from a privacy perspective, however it doesn't have to be. There are some group policies which can be set in order to limit the amount of data Microsoft collects and some to even outright disable it. There is also the issue of advertisements, in an operating system which you pay for (or any paid software for that matter) having advertisements in my opinion is unacceptable. Not only is this a possible breach of privacy, with most ads having trackers in order to deliver more "relevant" ads to you, but this also makes it less appealing to use and less user friendly.

So lets start with the advertisements, how can you disable them? If you have Windows 10 Pro or better, you have the option to use local group policy. This allows you to change settings which are otherwise unavailable, without having to dig around in the Windows registry. Open the run dialog with <inline-code>Windows Key + r</inline-code> and type <inline-code>gpedit.msc</inline-code>, once that opens you should be greeted with a window similar to the image below. Next you're going to need to edit the following policies:

![Group Policy 1](/images/blog/2019-03-14-privacy-and-windows-10/WindowsPrivacy1.png "Computer Configuration > Administrative Templates > Windows Components > Cloud Content"){:.blogImageInline}

Enable the following policies:
<pre><code class="language-html">Computer Configuration &gt; Administrative Templates &gt; Windows Components &gt; Cloud Content &gt; Do not show Windows tips;
Computer Configuration &gt; Administrative Templates &gt; Windows Components &gt; Cloud Content &gt; Turn off Microsoft consumer experiences;</code></pre>

<hr />

This one is just a personal opinion, but I also do not want the tips which are pulled from online and are often unhelpful and clutter the interface. So I disable them here:

![Group Policy 2](/images/blog/2019-03-14-privacy-and-windows-10/WindowsPrivacy2.png "Computer Configuration > Administrative Templates > Control Panel"){:.blogImageInline}

Disable the following policy:

<pre><code class="language-html">Computer Configuration &gt; Administrative Templates &gt; Control Panel &gt; Allow Online Tips;</code></pre>

<hr />

In regards to privacy there are a few different places where policy must be set, the first one here is up to personal preference for how you would like to set it. I personally set it to the lowest number I can, however it can be outright disabled. I found it to be annoying when disabled though, as every time you open an executable file Windows will give you a pop up saying that it "Cannot reach SmartScreen". For my PC I can only set it as low as <inline-code>1 - Basic</inline-code>, which sends security related data (SmartScreen) and some very basic diagnostic data such as error reports. However, I would recommend choosing a level of data which you are comfortable with, here are the descriptions of each from Microsoft:

  - __0 - Security [Enterprise Only]:__ Sends only a minimal amount of data to Microsoft, required to help keep Windows secure. Windows security components, such as the Malicious Software Removal Tool (MSRT) and Windows Defender may send data to Microsoft at this level, if enabled. Setting a value of 0 applies to devices running Enterprise, Education, IoT, or Windows Server editions only. Setting a value of 0 for other editions is equivalent to setting a value of 1.
  - __1 - Basic:__ Sends the same data as a value of 0, plus a very limited amount of diagnostic data, such as basic device info, quality-related data, and app compatibility info. Note that setting values of 0 or 1 will degrade certain experiences on the device.
  - __2 - Enhanced:__ Sends the same data as a value of 1, plus additional data such as how Windows, Windows Server, System Center, and apps are used, how they perform, and advanced reliability data.
  - __3 - Full:__ Sends the same data as a value of 2, plus advanced diagnostics data used to diagnose and fix problems with devices, including the files and content that may have caused a problem with the device.

![Group Policy 3](/images/blog/2019-03-14-privacy-and-windows-10/WindowsPrivacy3.png "Computer Configuration > Administrative Templates > Windows Components > Data Collection and Preview Builds"){:.blogImageInline}

Set the following policy to your desired level of telemetry:

<pre><code class="language-html">Computer Configuration &gt; Administrative Templates &gt; Windows Components &gt; Data Collection and Preview Builds &gt; Allow Telemetry;</code></pre>

<hr />

Another possibility is the disablement of application telemetry, in my experience this applies mostly to Microsoft Office. However there may be other applications which use this setting as well. I have not been able to find a whole lot of information regarding the type of data which is sent with this, however I would guess it is mostly diagnostic and usage related data. You can disable that here:

![Group Policy 4](/images/blog/2019-03-14-privacy-and-windows-10/WindowsPrivacy4.png "Computer Configuration > Administrative Templates > Windows Components > Application Compatibility"){:.blogImageInline}

Enable the following policy:

<pre><code class="language-html">Computer Configuration &gt; Administrative Templates &gt; Windows Components &gt; Application Compatibility &gt; Turn off Application Telemetry;</code></pre>

<hr />

The last place which I edit for this would be in regards to Cortana, as it is no secret that everything which is typed into this assistant is logged and sent to Microsoft for analysis. Microsoft also gives little choice in the matter to if you would like to use Cortana, and forced this "Smart Assistant" upon the users of Windows 10. However in my experience it has hindered more than it helps. The worst of these is the constant reminder to install Cortana on my mobile device, and it asks for a phone number to send you the link to do so every time you open the search menu. So here is how I disabled Cortana, and just make it a basic search tool:

![Group Policy 5](/images/blog/2019-03-14-privacy-and-windows-10/WindowsPrivacy5.png "Computer Configuration > Administrative Templates > Windows Components > Application Compatibility"){:.blogImageInline}

Disable the following policies:

<pre><code class="language-html">Computer Configuration &gt; Administrative Templates &gt; Windows Components &gt; Search &gt; Allow Cortana;
Computer Configuration &gt; Administrative Templates &gt; Windows Components &gt; Search &gt; Allow Cortana above lock screen;</code></pre>

Enable this last policy:

<pre><code class="language-html">Computer Configuration &gt; Administrative Templates &gt; Windows Components &gt; Search &gt; Don't search the web or display web results in Search;</code></pre>

<hr />

Overall I still believe that this amount of work should not be necessary for something as simple as privacy in the case of any software. However it is possible to have a sense of privacy if you are forced to use Windows 10 as I am, whether it be for work, gaming, or some other software you use being unavailable on any other platform. 