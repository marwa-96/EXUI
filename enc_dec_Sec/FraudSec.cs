using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enc_dec_Sec
{
    public class FraudSec
    {
        public static string base64Encode(string sData) // Encode    
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string base64Decode(string sData) //Decode    
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string FraudEnc(string SDAta)
        {
            string EncCase= "";
            string Case = base64Encode(SDAta);
            string _scretKey = base64Encode("(Cloudsoft5@@2016").Replace("=","");
            string _startscretKey = base64Encode("Cl5@2016").Replace("=", "");
            string MyCase = _startscretKey + Case + _scretKey;
            EncCase = base64Encode(MyCase);
            return EncCase;
        }
        public static string FraudDec(string SDAta)
        {
            string DecCase = "";
            string _scretKey = base64Encode("(Cloudsoft5@@2016").Replace("=", "");
            string _startscretKey = base64Encode("Cl5@2016").Replace("=", "");
            string COmpCase = base64Decode(SDAta);
            List<string> Decoderlist = COmpCase.Split(new string[] { _scretKey}, StringSplitOptions.None).ToList();
            List<string> EndingDecoderlist = Decoderlist[0].Split(new string[] { _startscretKey }, StringSplitOptions.None).ToList();
            DecCase = base64Decode(EndingDecoderlist[1]);

            return DecCase;
        }
    }
}
