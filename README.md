# CSE687 Test Harness
### Created for CSE687 at Syracuse University

!!!!! Note: Currently this experiences a bug with Visual Studio compilation where HttpConnector (part of c++ harness) will throw this compilation error and will sometimes need a few tries to build:

```
Error	C1090	PDB API call failed, error code '3': 
```

# Description
- The goal of this project was to create a backend C++ based test harness for testing functions within DLL's
- This was completed with a C# GUI using Microsofts UWP framework, a C++ backend application for completing the tests, and a golang API with a postgres backing for the data communication between the two.
  - API Repo located here [link](https://github.com/KaminFay/CSE687_API)
  
# Instalation Instructions:

- Download and extract the 1.0.0 release found here: [1.0.0](https://github.com/KaminFay/CSE687/releases/tag/1.0.0)
- Open windows powershell in admin mode and navigate into TestHarness_Gui_1.0.18.0_Test
- Run the Install.ps1 script:

```
PS C:\Users\Kamin> Set-ExecutionPolicy RemoteSigned
PS <\TestHarness_Gui_1.0.18.0.Test> .\Install.ps1
```

- Follow the prompts to accept the certificate and install the GUI

  ## Running after Install
  
  - The GUI will be found in the start menu and the backend will be found in the release folder here:
  
  ```
  \CSE687_Test_Harness_1.0.0\Test_Harness_Backend
  ```

# Testing Without Installing:

- Clone the repository:
  ```
  git clone https://github.com/KaminFay/CSE687.git
  ```
- Launch Visual studio and open both solutions:
  ```
  CSE687/guiApp/guiApp.sln
  CSE687/Testing/Top/Top.sln
  ```
- Ensure that within the C++ application the Test_Harness is the startup project and run the local windows debugger
- Once the C++ backend is started run the local windows debugger for the c# GUI as well and follow the Usage Instructions

# Usage Instructions:

- Sample DLL's and JSON files can be found [here](https://github.com/KaminFay/CSE687/tree/master/Dll_Source/dll_files):

```
CSE687/DLL_Source/dll_files
```

- Once the GUI and Backend are running a JSON file will need to be opened to layout the DLL's that are to be tested and their corresponding functions
  - This is done by selecting File->Open and selecting a .JSON file
  - Opening a JSON fill will present the user with a list of possible DLL's that were provided within that file
- Toggling a dll on in the GUI will open another file selector and allow the user to bind to the correct corresponding DLL
- From there the function list will populate, toggling one of the functions within this list will queue it for testing.
- Logging level can be selected above the run button
  - Level 1 -> Pass/Fail only
  - Level 2 -> Pass/Fail + Exception
  - Level 3 -> Pass/Fail + Exception + Start/End Times
- Clicking run will send the functions to the harness and the results will be displayed below

  ## Sample JSON Layout:

  ```
  {"dlls" : [
      {
          "Name":"auto_pass_delayed.dll",
          "Location":"C:\\Users\\Kamin\\Documents\\Garbage\\Dll_Tester\\",
          "Functions": [
              {
                "FuncName": "auto_pass_delayed",
                "PassedIn": "const int x",
                "ReturnType": "std::string"
              }
            ]
      },
      {
          "Name":"dll_long_delay.dll",
          "Location":"C:\\Users\\Kamin\\Documents\\GitHub\\CSE687\\Testing\\dll_files\\dll_long_delay.dll",
          "Functions": [
                  {
                    "FuncName":"Run_Test",
                    "PassedIn": "int y, int x",
                    "ReturnType": "int"
                  }
                ]
      },
      {
        "Name":"rng_exception_thrower.dll",
        "Location":"C:\\Users\\Kamin\\Documents\\GitHub\\CSE687\\Testing\\dll_files\\rng_exception_thrower.dll",
        "Functions": [
                {
                  "FuncName":"throw_exception",
                  "PassedIn": "int y, int x",
                  "ReturnType": "int"
                }
              ]
    }
  ]
  }
  ```
  
