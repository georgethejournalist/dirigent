SET CFG=%1
SET ARC=%2
IF "%1"=="" SET CFG=Debug
IF "%2"=="" SET ARC=x64\

copy ..\src\packages\CommandLineParser.1.9.71\lib\net35\CommandLine.dll  .\
copy ..\src\packages\log4net.2.0.3\lib\net35-full\log4net.dll  .\

copy ..\src\Dirigent.Agent.Core\bin\%ARC%%CFG%\Dirigent.Agent.Core.* .\

copy ..\src\Dirigent.Agent.Gui\bin\%ARC%%CFG%\Dirigent.Agent.Gui.* .\

copy ..\src\Dirigent.Common\bin\%ARC%%CFG%\Dirigent.Common.*  .\

copy ..\src\Dirigent.Master\bin\%ARC%%CFG%\Dirigent.Master.*  .\

copy ..\src\Dirigent.Agent.TrayApp\bin\%ARC%%CFG%\Dirigent.agent.* .\

copy ..\src\Dirigent.Agent.CmdLineCtrl\bin\%ARC%%CFG%\Dirigent.agentcmd.* .\

copy ..\README.md .\

