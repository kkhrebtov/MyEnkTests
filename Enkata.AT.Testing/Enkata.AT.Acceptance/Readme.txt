How to run acceptance tests
---------------------------

1. Take the required build for testing  from AT build server (\\SPWODEVBUILD01\AT_builds\DA_9.1.x)
2. Install NUnit 2.6.2 (\\ENOVIKOV-D1\network share\NUnit-2.6.2.msi) to target PC 
3. Prepare OS deloyment with corresponding Enkata Extensions 
4. Create new folder and put SystemSettings.xml, decryption files, calc.exe (32bit) and deployment to this folder
5. Replace SystemSettings.xml to corresponding version
5. Edit nunitSettings.xml
    a.	name="LocationInstalFile" – folder where is located AT.msi
    b.	name="LocationCalc" – path to calc.exe
    c.	name="DttPath" – output for DTT
    d.	name="TempFolder" – some tmp folder !!  must exists
    e.	name="PathToDecriptFile" – path to folder with pgp keys
    f.	name="NameSystemSettings" – should be "SystemSettings.xml"
    g.	name="ProjectPath" – path to deployed project
    h.	name="From" // stress scenario
    i.	name="To" // stress scenario
    j.	name="Users" // stress scenario
    k.	name="Delay" // stress scenario
    l.	name="ShareFolder" // extensions regression and stability only
    m.	another for Cigna tests
6.	Run NUnit (Acceptance Tests)!!!

SystemSettings.xml example:

<test name="Acceptance">
  <parameter name="LocationInstalFile" value="E:\AT"/>
  <parameter name="LocationCalc" value="E:\AT\calc.exe"/>
  <parameter name="DttPath" value="C:\DataOutput"/>
  <parameter name="TempFolder" value="c:\Temp"/>
  <parameter name="NameSystemSettings" value="SystemSettings.xml"/>  
  <parameter name="ProjectPath" value="E:\AT\Project3.OpenSpan"/>
</test>
