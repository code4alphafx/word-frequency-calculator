using NUnit.Framework;
using System;
using System.IO;
using WordFrequencyCounter.Extensions;

namespace WordFrequencyCounterTests
{
    public class ExtensionMethodTest
    {
        
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        
        public void GetMostFrequentUsedWords_WithInvalidInput_ShouldThrowException(int topNWords)
        {
            //arrange            
            var sr = new StreamReader(GenerateStreamFromString("random test string"));

            //act
            //assert
            Assert.Throws<InvalidOperationException>(() => sr.GetMostFrequentUsedWords(topNWords), "Incorrect value for parameter topNWords, value must be greater than 0");
        }

        [TestCase("Work published : you : may @ freely !share this work", 7)] //should return 7 words
        [TestCase("Data  . . .  ; ; ! @data", 2)] // should return 1 word
        [TestCase(". . .  ; ; ! @", 1)] //should return 0 words

        public void GetMostFrequentUsedWords_ShouldNotReturnMoreWords_ThanParamValueTopNWords(string content, int topNWords)
        {
            //arrange            
            var sr = new StreamReader(GenerateStreamFromString(content));

            //act
            var result = sr.GetMostFrequentUsedWords(topNWords);
            //assert
            Assert.LessOrEqual(result.Count, topNWords);            
        }

        [Test]
        public void GetMostFrequentUsedWords_WithSpecialCharsInWords_ShouldIgnoreSpecialChars()
        {
            //arrange  
            var content = "This is*Statement-With many.special@charachters,each#special~{chars}?<>;[should]&be(considered)&^%as£$!¬space"; //should return 14 words
            var sr = new StreamReader(GenerateStreamFromString(content));
            var exptectedCount = 14;

            //act
            var result = sr.GetMostFrequentUsedWords(exptectedCount); 

            //assert            
            Assert.AreEqual(exptectedCount, result.Count);
            Assert.That(result, Contains.Key("special").And.ContainValue(2));
            Assert.That(result, Contains.Key("this").And.ContainValue(1));
            Assert.That(result, Contains.Key("is").And.ContainValue(1));
            Assert.That(result, Contains.Key("statement").And.ContainValue(1));
            Assert.That(result, Contains.Key("with").And.ContainValue(1));
            Assert.That(result, Contains.Key("charachters").And.ContainValue(1));
            Assert.That(result, Contains.Key("each").And.ContainValue(1));
            Assert.That(result, Contains.Key("chars").And.ContainValue(1));
            Assert.That(result, Contains.Key("should").And.ContainValue(1));
            Assert.That(result, Contains.Key("be").And.ContainValue(1));
            Assert.That(result, Contains.Key("considered").And.ContainValue(1));
            Assert.That(result, Contains.Key("as").And.ContainValue(1));
            Assert.That(result, Contains.Key("space").And.ContainValue(1));
        }
                

        [Test]
        public void GetMostFrequentUsedWords_WithNoTopNWordsInput_ShouldReturn10OrLessWords()
        {
            //arrange  
            var content = "This is*Statement-With many.special@charachters,each#special~chars;should&be(considered)&^%as£$!¬space  this this with with"; //should return 10 words
            var sr = new StreamReader(GenerateStreamFromString(content));
            var exptectedCount = 10;

            //act
            var result = sr.GetMostFrequentUsedWords(); //no parameters passed

            //assert            
            Assert.AreEqual(exptectedCount, result.Count);
            Assert.That(result, Contains.Key("this").And.ContainValue(3));
            Assert.That(result, Contains.Key("with").And.ContainValue(3));
            Assert.That(result, Contains.Key("special").And.ContainValue(2));            
            Assert.That(result, Contains.Key("is").And.ContainValue(1));
            Assert.That(result, Contains.Key("many").And.ContainValue(1));            
            Assert.That(result, Contains.Key("statement").And.ContainValue(1));            
            Assert.That(result, Contains.Key("charachters").And.ContainValue(1));
            Assert.That(result, Contains.Key("each").And.ContainValue(1));
            Assert.That(result, Contains.Key("chars").And.ContainValue(1));
            Assert.That(result, Contains.Key("should").And.ContainValue(1));
        }


        #region helpers
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        #endregion
    }
}