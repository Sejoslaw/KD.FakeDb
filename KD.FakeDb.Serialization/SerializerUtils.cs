using System;

namespace KD.FakeDb.Serialization
{
    /// <summary>
    /// Contains some various functions used in reading / writing <see cref="IFakeDatabase"/>.
    /// </summary>
    public static class SerializerUtils
    {
        /// <summary>
        /// Tries to build object from given <see cref="Type"/> and cast it to given generic parameter. At first it will try and build default constructor with zero parameters.
        /// </summary>
        public static T TryToBuildObject<T>(Type type, object[] optionalArgs)
        {
            T newObject;

            try
            {
                newObject = (T)Activator.CreateInstance(type);
            }
            catch (Exception)
            {
                try
                {
                    newObject = (T)Activator.CreateInstance(type, optionalArgs);
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("Error when creating new object from Type: {0}", type));
                }
            }

            return newObject;
        }
    }
}