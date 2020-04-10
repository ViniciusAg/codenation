using System;
using System.Text.RegularExpressions;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {

        public string Crypt(string message)
        {
            if (message == null)
                throw new ArgumentNullException();

            if (message.Equals(""))
                return message;

            string alfabeto = "abcdefghijklmnopqrstuvwxyz";
            string cifrado = "";

            message = message.ToLower();
            foreach (var caractere in message)
            {
                if (alfabeto.IndexOf(caractere) > -1)
                {
                    if (alfabeto.IndexOf(caractere) + 3 < alfabeto.Length)
                        cifrado += alfabeto[alfabeto.IndexOf(caractere) + 3];
                    else
                        cifrado += alfabeto[(alfabeto.IndexOf(caractere) + 3) - (alfabeto.Length)];
                }
                else if (caractere == ' ' || char.IsNumber(caractere))
                {
                    cifrado += caractere;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return cifrado;
        }

        public string Decrypt(string cryptedMessage)
        {
            if (cryptedMessage == null)
                throw new ArgumentNullException();

            if (cryptedMessage.Equals(""))
                return cryptedMessage;                

            string alfabeto = "abcdefghijklmnopqrstuvwxyz";
            string decifrado = "";

            cryptedMessage = cryptedMessage.ToLower();
            foreach (var caractere in cryptedMessage)
            {
                if (alfabeto.IndexOf(caractere) > -1)
                {
                    if (alfabeto.IndexOf(caractere) - 3 >= 0)
                        decifrado += alfabeto[alfabeto.IndexOf(caractere) - 3];
                    else
                        decifrado += alfabeto[(alfabeto.IndexOf(caractere) - 3) + (alfabeto.Length)];
                }
                else if (caractere == ' ' || char.IsNumber(caractere))
                {
                    decifrado += caractere;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            return decifrado;
        }
    }
}