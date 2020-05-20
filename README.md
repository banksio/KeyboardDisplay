# Keyboard Display
[![Azure DevOps builds](https://img.shields.io/azure-devops/build/banksio/2d41c925-8f20-4136-87aa-f6236634c677/7.svg?style=flat)](https://banksio.visualstudio.com/KeyboardDisplay/_build?definitionId=7)
[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/banksio/2d41c925-8f20-4136-87aa-f6236634c677/7.svg?style=flat)](https://banksio.visualstudio.com/KeyboardDisplay/_build?definitionId=7)
![GitHub commit activity](https://img.shields.io/github/commit-activity/y/banksio/KeyboardDisplay.svg?style=flat)
[![GitHub (pre-)release](https://img.shields.io/github/release/banksio/KeyboardDisplay/all.svg)](https://github.com/banksio/KeyboardDisplay/releases/latest)
![GitHub All Releases](https://img.shields.io/github/downloads/banksio/KeyboardDisplay/total.svg?style=flat)

A small, simple Windows app to notify when caps lock, num lock or scroll lock are toggled, via a popup in the bottom right corner of the desktop. Useful if your keyboard doesn't have lock indicators.

![Main overlay](https://i.imgur.com/Y7zet1L.png)
![Settings window](https://i.imgur.com/Yz2OBji.png)

This application is currently in beta: There may still be some rough edges, but please feel free to test it and report any bugs found, or new features that may be beneficial!
## Features
* Fast and responsive.
* Modern design to fit Windows 10.
* Ignores clicks and does not steal focus from the foreground application.
* Hidden from taskbar.
* Under 1 MB in size.
## Install
### Prerequisites
* Windows 7 SP1, Windows 8.1, or Windows 10.
* Microsoft .NET Framework 4.7.2 (included with Windows 10 v1803 and later).
### Run
1. Download the setup application from the [latest release](https://github.com/banksio/KeyboardDisplay/releases/latest).
2. Run the setup and follow the instructions to install Keyboard Display.
3. Enjoy!

If you'd rather not install Keyboard Display, you can always grab the portable zip file to try it out. Just head to the releases page, download and extract the zip, and run the executable.
## Usage
Keyboard display will run in the background (starting on logon if specified during installation) and should pop up in the lower right hand corner of the screen whenever caps lock, num lock, or scroll lock are toggled.
A system tray icon will appear to allow graceful exiting from its context menu.

**If it doesn't work, please [submit a bug](https://github.com/banksio/KeyboardDisplay/issues).**

## Build
### Prerequisites
All above, and;
* Visual Studio 2012 or later.
* .NET SDK 4.7.2.
### Procedure
1. Clone or download the repository onto your system.
2. Open the solution in Visual Studio.
3. Build and run the project `KeyboardDisplay`!
