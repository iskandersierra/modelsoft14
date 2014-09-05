using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace ModelSoft.Framework.Test
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para StringExtensionsTest y se pretende que
    ///contenga todas las pruebas unitarias StringExtensionsTest.
    ///</summary>
  [TestClass()]
  public class StringExtensionsTest
  {

    private TestContext testContextInstance;

    /// <summary>
    ///Obtiene o establece el contexto de la prueba que proporciona
    ///la información y funcionalidad para la ejecución de pruebas actual.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Atributos de prueba adicionales
    // 
    //Puede utilizar los siguientes atributos adicionales mientras escribe sus pruebas:
    //
    //Use ClassInitialize para ejecutar código antes de ejecutar la primera prueba en la clase 
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup para ejecutar código después de haber ejecutado todas las pruebas en una clase
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize para ejecutar código antes de ejecutar cada prueba
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion

    [TestMethod]
    public void ToPlainTextTest()
    {
      var text = @"
àáâãäåæāăą
ÀÁÂÃÄÅÆĀĂĄ
èéêëēĕėęě
ÈÉÊËĒĔĖĘĚƎƏƐ
ìíîïĩīĭįı
ÌÍÎÏĨĪĬĮİƖƗ
ĳ
Ĳ
ðòóôõöøōŏőơ
ÒÓÔÕÖØŌŎŐƟƠ
œ
Œ
ƣ
Ƣ
ùúûüũūŭůűųư
ÙÚÛÜŨŪŬŮŰŲƯ

ƀƃƅ
ƁƂƄ
çćĉċčƈ
ÇĆĈĊČƆƇ
ďđƌƍ
ÐĎĐƉƊƋ
ƒ
Ƒ
ĝğġģ
ĜĞĠĢƓƔ
ĥħ
ƕ
ĤĦ
ĵ
Ĵ
ķĸƙ
ĶƘ
ĺļľŀłƚƛ
ĹĻĽĿŁ
Ɯ
ñńņňŉŋƞ
ÑŃŅŇŊƝ
ƥ
Ƥ
ŕŗř
ŔŖŘ
śŝşšßſƨ
ŚŜŞŠƧƩƪ
þţťŧƫƭ
ÞŢŤŦƬƮ
Ʋ
ŵ
Ŵ
ýŷÿƴ
ÝŶŸƳ
Ʀ
źżžƶƹƺ
ŹŻŽƵƷƸ";
      var expectedText = @"
aaaaaaaaaa
AAAAAAAAAA
eeeeeeeee
EEEEEEEEEEEE
iiiiiiiii
IIIIIIIIIII
ij
IJ
ooooooooooo
OOOOOOOOOOO
oe
OE
oi
OI
uuuuuuuuuuu
UUUUUUUUUUU

bbb
BBB
cccccc
CCCCCCC
dddd
DDDDDD
f
F
gggg
GGGGGG
hh
hv
HH
j
J
kkk
KK
lllllll
LLLLL
m
nnnnnnn
NNNNNN
p
P
rrr
RRR
sssssss
SSSSSSS
tttttt
TTTTTT
V
w
W
yyyy
YYYY
yr
zzzzzz
ZZZZZZ";
      var plainText = text.ToPlainText();
      Assert.AreEqual(expectedText, plainText);
    }
  }
}
