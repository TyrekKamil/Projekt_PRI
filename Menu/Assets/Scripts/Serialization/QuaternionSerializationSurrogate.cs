using System.Numerics;
using System.Runtime.Serialization;

public class QuaternionSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Quaternion quaternion = (Quaternion)obj;
        info.AddValue("X", quaternion.X);
        info.AddValue("Y", quaternion.Y);
        info.AddValue("Z", quaternion.Z);
        info.AddValue("W", quaternion.W);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Quaternion quaternion = (Quaternion)obj;
        quaternion.X = (float)info.GetValue("X", typeof(float));
        quaternion.Y = (float)info.GetValue("Y", typeof(float));
        quaternion.Z = (float)info.GetValue("Z", typeof(float));
        quaternion.W = (float)info.GetValue("W", typeof(float));
        obj = quaternion;
        return obj;
    }
}
