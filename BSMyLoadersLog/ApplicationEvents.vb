Imports BSMyLoadersLog.LoadersClass

' ReSharper disable once CheckNamespace
Namespace My

    ' The following events are availble for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        ''' <summary>
        ''' Sets the visual styles, text display styles, and current principal for the main application thread (if the application uses Windows authentication), and initializes the splash screen, if defined.
        ''' </summary>
        ''' <param name="commandLineArgs">A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see langword="String" />, containing the command-line arguments as strings for the current application.</param>
        ''' <returns>A <see cref="T:System.Boolean" /> indicating if application startup should continue.</returns>
' ReSharper disable once ParameterHidesMember
        Protected Overrides Function OnInitialize(ByVal commandLineArgs As ObjectModel.ReadOnlyCollection(Of String)) As Boolean
            Dim objf As New BSFileSystem
            Try
                Dim appDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\BurnSoft\MLL"
                Dim appdatapathExists As Boolean = objf.DirectoryExists(appDataPath)
                APPLICATION_PATH = Windows.Forms.Application.StartupPath
                APPLICATION_PATH_DATA = APPLICATION_PATH
                If appdatapathExists Then
                    If objf.FileExists(appDataPath & "\" & DATABASE_NAME) Then
                        APPLICATION_PATH_DATA = appDataPath
                    End If
                End If
                AppDomain.CurrentDomain.SetData("DataDirectory", APPLICATION_PATH_DATA)
                MyLogFile = APPLICATION_PATH_DATA & "\err.log"
                Return MyBase.OnInitialize(commandLineArgs)
            Catch ex As Exception
' ReSharper disable once ConvertToConstant.Local
                Dim strProcedure As String = "OnInitialize"
                Call LogError("My.MyApplication", strProcedure, Err.Number, ex.Message.ToString)
            End Try
        End Function
    End Class

End Namespace

