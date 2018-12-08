# Keyboard Display
[![AppVeyor](https://img.shields.io/appveyor/ci/banksio/KeyboardDisplay.svg)](https://ci.appveyor.com/project/banksio/keyboarddisplay)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/banksio/KeyboardDisplay.svg)](https://ci.appveyor.com/project/banksio/keyboarddisplay/build/tests)
[![GitHub issues](https://img.shields.io/github/issues/banksio/KeyboardDisplay.svg)](https://github.com/banksio/KeyboardDisplay/issues)
[![GitHub (pre-)release](https://img.shields.io/github/release/banksio/KeyboardDisplay/all.svg)](https://github.com/banksio/KeyboardDisplay/releases/latest)
[![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/banksio/KeyboardDisplay.svg)](https://github.com/banksio/KeyboardDisplay/releases/latest)
[![GitHub last commit](https://img.shields.io/github/last-commit/banksio/KeyboardDisplay.svg)](https://github.com/banksio/KeyboardDisplay/commits)
[![GitHub license](https://img.shields.io/github/license/banksio/KeyboardDisplay.svg)](https://github.com/banksio/KeyboardDisplay)

A small, simple Windows app to notify when caps lock, num lock or scroll lock are toggled, via a popup in the bottom right corner of the desktop.

This application is currently in alpha: It is not recommended to rely on this program right now, but please feel free to test it and report any bugs found, or new features that may be beneficial!
## Install
### Prerequisites
* Windows 7 SP1, Windows 8.1, or later.
* Microsoft .NET Framework 4.7.2 (included with Windows 10 v1803 and later).
### Run
1. Download the [latest release](https://github.com/banksio/KeyboardDisplay/releases/latest).
2. Extract the zip folder.
3. Run the executable! (Installer [coming soon](https://github.com/banksio/KeyboardDisplay/issues/6).)
## Usage
Keyboard display will run in the background (soon to [start on logon](https://github.com/banksio/KeyboardDisplay/issues/7)) and should pop up in the lower right hand corner of the screen whenever caps lock, num lock, or scroll lock are toggled.
A system tray icon will appear to allow graceful exiting from its context menu.
## Build
### Prerequisites
* All above, and;
* Visual Studio 2012 or later.
* .NET SDK 4.7.2.
### Procedure
1. Clone or download the repository onto your system.
2. Open the solution in Visual Studio.
3. Build and run the project `KeyboardDisplay`!
