@echo off
for %%a in (D:\BERT\BertScout2020Data\Documents\2020*.json) do %LOCALAPPDATA%\Android\sdk\platform-tools\adb.exe push "%%a" /storage/sdcard0/Documents/
pause
