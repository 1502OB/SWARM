# Autodesk.AutoCAD.Internal.Render.RapidRT

NAMESPACE: Autodesk.AutoCAD.Internal.Render.RapidRT
--------------------------------------------------------------------------------

[CLASS] PairedImage
Full Name: Autodesk.AutoCAD.Internal.Render.RapidRT.PairedImage
Implements: IDisposable

  CONSTRUCTORS:
    new PairedImage()

  PROPERTIES:
        [SKIPPED PROPERTY] ManagedBitMap: Could not load file or assembly 'System.Drawing.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. The system cannot find the file specified.
    Image* UnmanagedBitmap { get; set; }

  METHODS:
    void Dispose()

--------------------------------------------------------------------------------

[CLASS] RenderUtil
Full Name: Autodesk.AutoCAD.Internal.Render.RapidRT.RenderUtil

  CONSTRUCTORS:
    new RenderUtil()

  METHODS:
    static ErrorStatus CreateCustomPreset(string presetName, string presetDescription, string basePresetName)
    static ErrorStatus DeleteCustomPreset(string presetName)
    static bool DoModalRenderSaveFileDlg(ref string pStrPath, StringCollection filterExtName, StringCollection filterExt, string caption, string invalidFileName)
    static string GetSceneResString(int id)
    static byte[] GetSceneResXMLBlob(int id)
    static PairedImage GetViewportImage()
    static ErrorStatus RenameCustomPreset(string presetName, string newPresetName)
    static bool SaveImage(string fileName, string strFormat, PairedImage imagePair)
    static bool SaveRenderJobImage(string fileName, string strFormat, int jobIndex, Document pDoc)

--------------------------------------------------------------------------------

