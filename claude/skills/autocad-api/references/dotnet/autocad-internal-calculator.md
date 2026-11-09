# Autodesk.AutoCAD.Internal.Calculator

NAMESPACE: Autodesk.AutoCAD.Internal.Calculator
--------------------------------------------------------------------------------

[ENUM] AngleUnitsEnum
Full Name: Autodesk.AutoCAD.Internal.Calculator.AngleUnitsEnum
Inherits: Enum

  Values:
    - Degree = 0
    - DegreeMinSec = 1
    - Grad = 2
    - Radian = 3
    - Surveyor = 4
    - None = -1

--------------------------------------------------------------------------------

[CLASS] CalcContext
Full Name: Autodesk.AutoCAD.Internal.Calculator.CalcContext
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new CalcContext()

  PROPERTIES:
    AngleUnitsEnum AngleUnits { get; set; }
    CalcHostServices HostServices { get; }

--------------------------------------------------------------------------------

[CLASS] CalcEngine
Full Name: Autodesk.AutoCAD.Internal.Calculator.CalcEngine
Inherits: DisposableWrapper

  PROPERTIES:
    CalcGlobalVariableBag GlobalVariableCollection { get; }
    CalcGlobalVariableServices GlobalVariableServices { get; set; }

  METHODS:
    bool AddGlobalVariable(string variableName, CalcValueType variableValue)
    bool EvaluateExpression(string expression, CalcContext context, CalcResult result)
    bool EvaluateExpressionDisableSnap(string expression, CalcContext context, CalcResult result)
    static CalcEngine GetEngine()
    bool RemoveGlobalVariable(string variableName)

--------------------------------------------------------------------------------

[CLASS] CalcGlobalVariableBag
Full Name: Autodesk.AutoCAD.Internal.Calculator.CalcGlobalVariableBag
Inherits: DisposableWrapper
Implements: IEnumerable, IEnumerator

  CONSTRUCTORS:
    new CalcGlobalVariableBag(AcCalGlobalVarCollection* pvars)

  PROPERTIES:
    object Current { get; }

  METHODS:
    IEnumerator GetEnumerator()
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[CLASS] CalcGlobalVariableServices
Full Name: Autodesk.AutoCAD.Internal.Calculator.CalcGlobalVariableServices
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new CalcGlobalVariableServices()

  PROPERTIES:
    OnGlobalVariableModifiedHandler GlobalVariableModifiedEvent { get; set; }

--------------------------------------------------------------------------------

[CLASS] CalcHostServices
Full Name: Autodesk.AutoCAD.Internal.Calculator.CalcHostServices
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new CalcHostServices()

  PROPERTIES:
    OnInteractiveFunctionEpilogEventHandler FuncEpilogEvent { get; set; }
    OnInteractiveFunctionPrologEventHandler FuncPrologEvent { get; set; }
    OnWriteStringEventHandler WriteStringEvent { get; set; }

  METHODS:
    void SetQCState(bool value)

--------------------------------------------------------------------------------

[CLASS] CalcResult
Full Name: Autodesk.AutoCAD.Internal.Calculator.CalcResult
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new CalcResult(CalcValueType Value)

  PROPERTIES:
    string DWGResultString { get; }
    CalcValueType Result { get; set; }
    string ResultString { get; }
    ValueTypeEnum Type { get; set; }

--------------------------------------------------------------------------------

[STRUCT] CalcValueType
Full Name: Autodesk.AutoCAD.Internal.Calculator.CalcValueType

  PROPERTIES:
    double DistanceUnit { get; set; }
    DistanceBaseUnitType DistanceUnitType { get; }
    double R { get; set; }
    ValueTypeEnum Type { get; set; }
    XYZPoint V { get; set; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    static CalcValueType Parse(string string)
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] FuncPrologEpilogEventArgs
Full Name: Autodesk.AutoCAD.Internal.Calculator.FuncPrologEpilogEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new FuncPrologEpilogEventArgs(string FuncName, CalcResult Result)

--------------------------------------------------------------------------------

[CLASS] GlobalVariable
Full Name: Autodesk.AutoCAD.Internal.Calculator.GlobalVariable
Inherits: DisposableWrapper

  CONSTRUCTORS:
    new GlobalVariable(AcCalGlobalVar* umgd)

  PROPERTIES:
    string Name { get; }
    CalcValueType Value { get; }

--------------------------------------------------------------------------------

[CLASS] GlobalVariableEventArgs
Full Name: Autodesk.AutoCAD.Internal.Calculator.GlobalVariableEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new GlobalVariableEventArgs(string VariableName, CalcValueType VariableValue, bool IsNewVariable)

--------------------------------------------------------------------------------

[CLASS] IOStringEventArgs
Full Name: Autodesk.AutoCAD.Internal.Calculator.IOStringEventArgs
Inherits: EventArgs

  CONSTRUCTORS:
    new IOStringEventArgs(string IOString)

  PROPERTIES:
    string IOString { get; set; }

--------------------------------------------------------------------------------

[CLASS] OnGlobalVariableModifiedHandler
Full Name: Autodesk.AutoCAD.Internal.Calculator.OnGlobalVariableModifiedHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new OnGlobalVariableModifiedHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(GlobalVariableEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(GlobalVariableEventArgs e)

--------------------------------------------------------------------------------

[CLASS] OnInteractiveFunctionEpilogEventHandler
Full Name: Autodesk.AutoCAD.Internal.Calculator.OnInteractiveFunctionEpilogEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new OnInteractiveFunctionEpilogEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(FuncPrologEpilogEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(FuncPrologEpilogEventArgs e)

--------------------------------------------------------------------------------

[CLASS] OnInteractiveFunctionPrologEventHandler
Full Name: Autodesk.AutoCAD.Internal.Calculator.OnInteractiveFunctionPrologEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new OnInteractiveFunctionPrologEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(FuncPrologEpilogEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(FuncPrologEpilogEventArgs e)

--------------------------------------------------------------------------------

[CLASS] OnWriteStringEventHandler
Full Name: Autodesk.AutoCAD.Internal.Calculator.OnWriteStringEventHandler
Inherits: MulticastDelegate

  CONSTRUCTORS:
    new OnWriteStringEventHandler(object A_0, IntPtr A_1)

  METHODS:
    IAsyncResult BeginInvoke(IOStringEventArgs e, AsyncCallback callback, object obj)
    void EndInvoke(IAsyncResult result)
    void Invoke(IOStringEventArgs e)

--------------------------------------------------------------------------------

[STRUCT] Unit
Full Name: Autodesk.AutoCAD.Internal.Calculator.Unit

  CONSTRUCTORS:
    new Unit()

--------------------------------------------------------------------------------

[ENUM] ValueTypeEnum
Full Name: Autodesk.AutoCAD.Internal.Calculator.ValueTypeEnum
Inherits: Enum

  Values:
    - VectorType = 0
    - RealType = 1
    - IntType = 2
    - NoType = 3

--------------------------------------------------------------------------------

[STRUCT] XYZPoint
Full Name: Autodesk.AutoCAD.Internal.Calculator.XYZPoint

  CONSTRUCTORS:
    new XYZPoint(double X, double Y, double Z)

  PROPERTIES:
    double X { get; set; }
    double Y { get; set; }
    double Z { get; set; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()

--------------------------------------------------------------------------------

