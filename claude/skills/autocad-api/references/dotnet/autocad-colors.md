# Autodesk.AutoCAD.Colors

NAMESPACE: Autodesk.AutoCAD.Colors
--------------------------------------------------------------------------------

[CLASS] Color
Full Name: Autodesk.AutoCAD.Colors.Color
Inherits: DisposableWrapper
Implements: IComparable, ICloneable

  CONSTRUCTORS:
    new Color()

  PROPERTIES:
    byte Blue { get; }
    string BookName { get; }
    short ColorIndex { get; }
    ColorMethod ColorMethod { get; }
    string ColorName { get; }
    string ColorNameForDisplay { get; }
    Color ColorValue { get; }
    string Description { get; }
    string DictionaryKey { get; }
    int DictionaryKeyLength { get; }
    EntityColor EntityColor { get; }
    string Explanation { get; }
    byte Green { get; }
    bool HasBookName { get; }
    bool HasColorName { get; }
    bool IsByAci { get; }
    bool IsByBlock { get; }
    bool IsByColor { get; }
    bool IsByLayer { get; }
    bool IsByPen { get; }
    bool IsForeground { get; }
    bool IsNone { get; }
    short PenIndex { get; }
    byte Red { get; }

  METHODS:
    void Audit(AuditInfo auditInfo)
    object Clone()
    int CompareTo(object obj)
    static Color DwgIn(DwgFiler inputFiler)
    void DwgOut(DwgFiler outputFiler)
    static Color DxfIn(DxfFiler inputFiler, int groupCodeOffset)
    void DxfOut(DxfFiler outputFiler, int groupCodeOffset)
    bool Equals(object obj)
    [SKIPPED METHOD] FromColor: Could not load file or assembly 'PresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. The system cannot find the file specified.
    static Color FromColor(Color value)
    static Color FromColorIndex(ColorMethod colorMethod, short colorIndex)
    static Color FromDictionaryName(string name)
    static Color FromEntityColor(EntityColor eclr)
    static Color FromNames(string colorName, string bookName)
    static Color FromRgb(byte red, byte green, byte blue)
    static Color GetColorValue(short colorIndex, Color backgroundColor)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] ColorConverter
Full Name: Autodesk.AutoCAD.Colors.ColorConverter
Inherits: TypeConverter

  CONSTRUCTORS:
    new ColorConverter()

  METHODS:
    bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)

--------------------------------------------------------------------------------

[ENUM] ColorMethod
Full Name: Autodesk.AutoCAD.Colors.ColorMethod
Inherits: Enum

  Values:
    - ByLayer = 192
    - ByBlock = 193
    - ByColor = 194
    - ByAci = 195
    - ByPen = 196
    - Foreground = 197
    - LayerOff = 198
    - LayerFrozen = 199
    - None = 200

--------------------------------------------------------------------------------

[STRUCT] EntityColor
Full Name: Autodesk.AutoCAD.Colors.EntityColor

  CONSTRUCTORS:
    new EntityColor(byte red, byte green, byte blue)
    new EntityColor(ColorMethod value, int index)
    new EntityColor(ColorMethod value)
    new EntityColor(int trueColor)

  PROPERTIES:
    byte Blue { get; }
    short ColorIndex { get; }
    ColorMethod ColorMethod { get; }
    byte Green { get; }
    bool IsByAci { get; }
    bool IsByBlock { get; }
    bool IsByColor { get; }
    bool IsByLayer { get; }
    bool IsByPen { get; }
    bool IsForeground { get; }
    bool IsLayerFrozen { get; }
    bool IsLayerFrozenOrOff { get; }
    bool IsLayerOff { get; }
    bool IsNone { get; }
    int LayerIndex { get; }
    short PenIndex { get; }
    byte Red { get; }
    int TrueColor { get; }

  METHODS:
    static byte LookUpAci(byte red, byte green, byte blue)
    static int LookUpRgb(byte colorIndex)

--------------------------------------------------------------------------------

[CLASS] EntityColorCollection
Full Name: Autodesk.AutoCAD.Colors.EntityColorCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new EntityColorCollection(EntityColor[] values)
    new EntityColorCollection(int capacity)
    new EntityColorCollection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    EntityColor Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(EntityColor value)
    void AddRange(EntityColor[] range)
    void Clear()
    bool Contains(EntityColor value)
    void CopyTo(EntityColor[] array, int index)
    EntityColorCollectionEnumerator GetEnumerator()
    int IndexOf(EntityColor value)
    void Insert(int index, EntityColor value)
    void Remove(EntityColor value)
    void RemoveAt(int index)
    EntityColor[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] EntityColorCollectionEnumerator
Full Name: Autodesk.AutoCAD.Colors.EntityColorCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new EntityColorCollectionEnumerator(EntityColorCollection col)

  PROPERTIES:
    EntityColor Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[STRUCT] Transparency
Full Name: Autodesk.AutoCAD.Colors.Transparency

  CONSTRUCTORS:
    new Transparency(byte alpha)
    new Transparency(TransparencyMethod method)

  PROPERTIES:
    byte Alpha { get; }
    bool IsByAlpha { get; }
    bool IsByBlock { get; }
    bool IsByLayer { get; }
    bool IsClear { get; }
    bool IsInvalid { get; }
    bool IsSolid { get; }

  METHODS:
    bool Equals(object obj)
    int GetHashCode()
    string ToString()
    string ToString(IFormatProvider provider)

--------------------------------------------------------------------------------

[CLASS] TransparencyCollection
Full Name: Autodesk.AutoCAD.Colors.TransparencyCollection
Implements: IList, ICollection, IEnumerable

  CONSTRUCTORS:
    new TransparencyCollection(Transparency[] values)
    new TransparencyCollection(int capacity)
    new TransparencyCollection()

  PROPERTIES:
    int Capacity { get; set; }
    int Count { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    Transparency Item { get; set; }
    object SyncRoot { get; }

  METHODS:
    int Add(Transparency value)
    void AddRange(Transparency[] range)
    void Clear()
    bool Contains(Transparency value)
    void CopyTo(Transparency[] array, int index)
    TransparencyCollectionEnumerator GetEnumerator()
    int IndexOf(Transparency value)
    void Insert(int index, Transparency value)
    void Remove(Transparency value)
    void RemoveAt(int index)
    Transparency[] ToArray()
    void TrimToSize()

  CONSTANTS/STATIC FIELDS:
    const int DefaultSize = 10

--------------------------------------------------------------------------------

[CLASS] TransparencyCollectionEnumerator
Full Name: Autodesk.AutoCAD.Colors.TransparencyCollectionEnumerator
Implements: IEnumerator

  CONSTRUCTORS:
    new TransparencyCollectionEnumerator(TransparencyCollection col)

  PROPERTIES:
    Transparency Current { get; }

  METHODS:
    bool MoveNext()
    void Reset()

--------------------------------------------------------------------------------

[ENUM] TransparencyMethod
Full Name: Autodesk.AutoCAD.Colors.TransparencyMethod
Inherits: Enum

  Values:
    - ByLayer = 0
    - ByBlock = 1
    - ByAlpha = 2
    - ErrorValue = 3

--------------------------------------------------------------------------------

