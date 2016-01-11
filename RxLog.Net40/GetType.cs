using System;

partial class TypeUtility
{
    public static Type GetType(string typeName)
    {
        var type = Type.GetType(typeName);
        if (type != null) return type;

        return Type.GetType("RxLog." + typeName);
    }
}

