;#DropDB MarbidEasyTest

#Application Marbid.Win
#Application Marbid.Web

*FillForm
 User Name = Admin
 Password = Ia20090207
*Action Log On

#IfDef Marbid.Win

*Action About...
 
*Action OK

#EndIf

#IfDef Marbid.Web
*Action Navigation(My Details)
#EndIf
