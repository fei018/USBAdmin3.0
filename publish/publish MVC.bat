@echo " start MVC ? " 
@pause

echo aaa >> \\hhdmstest02.hiphing.nws\USBAdmin\web.config

ping 127.0.0.1 -n 5 >nul

Del "\\hhdmstest02.hiphing.nws\USBAdmin\*" /Q
robocopy "MVC" "\\hhdmstest02.hiphing.nws\USBAdmin" /MIR /R:0 /NP /TEE /XD Update

pause