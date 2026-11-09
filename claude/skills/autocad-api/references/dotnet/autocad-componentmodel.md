# Autodesk.AutoCAD.ComponentModel

NAMESPACE: Autodesk.AutoCAD.ComponentModel
--------------------------------------------------------------------------------

[INTERFACE] IPropertyProvider
Full Name: Autodesk.AutoCAD.ComponentModel.IPropertyProvider

  METHODS:
    PropertyDescriptorCollection GetProperties(object instance)

--------------------------------------------------------------------------------

[INTERFACE] ITypeDescriptor
Full Name: Autodesk.AutoCAD.ComponentModel.ITypeDescriptor

  PROPERTIES:
    bool HasPerInstancePropertyProviders { get; }

  METHODS:
    void AddPerInstancePropertyProvider(IPropertyProvider pp)
    void AddProperty(PropertyDescriptor prop)
    ReadOnlyCollection<IPropertyProvider> GetPerInstancePropertyProviders()
    PropertyDescriptorCollection ModifyPropertyCollection(PropertyDescriptorCollection defaultProps, object instance)
    void RemovePerInstancePropertyProvider(IPropertyProvider pp)
    void RemoveProperty(PropertyDescriptor prop)

--------------------------------------------------------------------------------

[ABSTRACT CLASS] PropertyDescriptorBase`2
Full Name: Autodesk.AutoCAD.ComponentModel.PropertyDescriptorBase`2
Inherits: PropertyDescriptor
Type Parameters: <TComponent, TPropertyValue>

  CONSTRUCTORS:
    new PropertyDescriptorBase`2(string name)
    new PropertyDescriptorBase`2(string name, Attribute[] att)

  PROPERTIES:
    Type ComponentType { get; }
    string DisplayName { get; }
    bool IsReadOnly { get; }
    Type PropertyType { get; }

  METHODS:
    bool CanResetValue(object component)
    void ResetValue(object component)
    bool ShouldSerializeValue(object component)

--------------------------------------------------------------------------------

[CLASS] PropertyProviderAttribute
Full Name: Autodesk.AutoCAD.ComponentModel.PropertyProviderAttribute
Inherits: Attribute

  CONSTRUCTORS:
    new PropertyProviderAttribute(Type type)

  PROPERTIES:
    Type PropertyProviderType { get; }

--------------------------------------------------------------------------------

[CLASS] TypeDescriptionProvider`1
Full Name: Autodesk.AutoCAD.ComponentModel.TypeDescriptionProvider`1
Inherits: TypeDescriptionProvider
Type Parameters: <T>

  CONSTRUCTORS:
    new TypeDescriptionProvider`1()

  METHODS:
    ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)

--------------------------------------------------------------------------------

[CLASS] TypeDescriptor
Full Name: Autodesk.AutoCAD.ComponentModel.TypeDescriptor
Implements: ITypeDescriptor

  CONSTRUCTORS:
    new TypeDescriptor(Type type)

  PROPERTIES:
    bool HasPerInstancePropertyProviders { get; }

  METHODS:
    void AddPerInstancePropertyProvider(IPropertyProvider pp)
    void AddProperty(PropertyDescriptor prop)
    ReadOnlyCollection<IPropertyProvider> GetPerInstancePropertyProviders()
    PropertyDescriptorCollection ModifyPropertyCollection(PropertyDescriptorCollection defaultProps, object instance)
    void RemovePerInstancePropertyProvider(IPropertyProvider pp)
    void RemoveProperty(PropertyDescriptor prop)

--------------------------------------------------------------------------------

[CLASS] TypeManager
Full Name: Autodesk.AutoCAD.ComponentModel.TypeManager

  PROPERTIES:
    static TypeManager Instance { get; }
    ITypeDescriptor Item { get; }

--------------------------------------------------------------------------------

