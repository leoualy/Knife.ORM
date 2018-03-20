:注册SQLDMO.dll 的批处理

: 将当前目录下的SQLDMO.dll 复制到 C:\ProgramFiles\Microsoft SQL Server\80\Tools\Binn下
copy .\Com\SQLDMO.DLL  C:\ProgramFiles\Microsoft SQL Server\80\Tools\Binn\SQLDMO.DLL

: 注册该Com组件
regsvr32"C:\ProgramFiles\Microsoft SQL Server\80\Tools\Binn\SQLDMO.DLL"