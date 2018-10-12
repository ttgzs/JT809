﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT809.Protocol;
using JT809.Protocol.JT809Extensions;
using JT809.Protocol.JT809MessageBody;
using JT809.Protocol.JT809Encrypt;

namespace JT809.Protocol.Test.JT809Packages
{
    public class JT809_0x1001EncryptPackageTest
    {
        public JT809_0x1001EncryptPackageTest()
        {
            JT809GlobalConfig.Instance.SetEncrypt(new JT809EncryptImpl(new JT809Configs.JT809EncryptOptions()
            {
                IA1 = 20000000,
                IC1 = 20000000,
                M1 = 30000000
            }));
        }

        [Fact]
        public void Test1()
        {
            JT809Package jT809Package = new JT809Package();
            jT809Package.Header = new JT809Header
            {
                EncryptFlag= JT809Header_Encrypt.Common,
                MsgSN= 133,
                EncryptKey= 256178,
                BusinessType= JT809Enums.JT809BusinessType.主链路登录请求消息,
                MsgGNSSCENTERID= 20180920,
            };
            JT809_0x1001 jT809_0X1001 = new JT809_0x1001();
            jT809_0X1001.UserId = 20180920;
            jT809_0X1001.Password = "20180920";
            jT809_0X1001.DownLinkIP = "127.0.0.1";
            jT809_0X1001.DownLinkPort = 809;
            jT809Package.Bodies = jT809_0X1001;
            var hex = JT809Serializer.Serialize(jT809Package).ToHexString();
            //"5B 00 00 00 48 00 00 00 85 10 01 01 33 EF B8 01 00 00 01 00 00 16 BB D3 7D 9C C4 90 0C 77 DC 78 F8 67 65 27 D8 AE 12 24 3C FB 64 CC 2F BA 61 9A EF AD 33 AC CB 32 56 F6 7B FF 19 DF 33 09 78 41 09 86 65 70 3F 2E B5 5D"
        }

        [Fact]
        public void Test2()
        {
            var bytes = "5B000000480000008510010133EFB8010000010003E8B2D37D9CC4900C77DC78F8676527D8AE12243CFB64CC2FBA619AEFAD33ACCB3256F67BFF19DF33097841098665703FE36E5D".ToStr2HexBytes();
            JT809Package jT809Package = JT809Serializer.Deserialize(bytes);
            Assert.Equal(JT809Header_Encrypt.Common, jT809Package.Header.EncryptFlag);
            Assert.Equal((uint)256178, jT809Package.Header.EncryptKey);
            Assert.Equal((uint)72, jT809Package.Header.MsgLength);
            Assert.Equal((uint)133, jT809Package.Header.MsgSN);
            Assert.Equal((uint)20180920, jT809Package.Header.MsgGNSSCENTERID);
            Assert.Equal(JT809Enums.JT809BusinessType.主链路登录请求消息, jT809Package.Header.BusinessType);
            Assert.Equal("010000", jT809Package.Header.Version);
            JT809_0x1001 jT809_0X1001 = (JT809_0x1001)jT809Package.Bodies;
            Assert.Equal((uint)20180920, jT809_0X1001.UserId);
            Assert.Equal("20180920", jT809_0X1001.Password);
            Assert.Equal("127.0.0.1", jT809_0X1001.DownLinkIP);
            Assert.Equal((ushort)809, jT809_0X1001.DownLinkPort);
        }
    }
}
