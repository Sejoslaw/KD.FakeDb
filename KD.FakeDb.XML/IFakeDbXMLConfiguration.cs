﻿using KD.FakeDb.Serialization;
using System.Xml;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// XML configuration for <see cref="FakeDbSerializer{TReader, TWriter}"/>.
    /// </summary>
    public interface IFakeDbXMLConfiguration : IFakeDbSerializerConfiguration<XmlReader, XmlWriter>
    {
    }
}