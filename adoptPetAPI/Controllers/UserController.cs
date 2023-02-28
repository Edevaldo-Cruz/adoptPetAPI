using adoptPetAPI.Context;
using adoptPetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System;

namespace adoptPetAPI.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class UserController : Controller
    {
        private readonly AdoptPetContext _context;

        public UserController(AdoptPetContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var urses = _context.Users;
            return Ok(urses);
        }

        [HttpGet("GetUserById")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("GetUserByName")]
        public IActionResult GetByName(string name)
        {
            var user = _context.Users.Where(x => x.Name.Contains(name));
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, User user)
        {
            var userBank = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            userBank.Name = user.Name;
            userBank.DateOfBirth = user.DateOfBirth;
            userBank.Password = user.Password;
            userBank.Email = user.Email;
            userBank.Address = user.Address;
            userBank.Telephone = user.Telephone;

            _context.Users.Update(userBank);
            _context.SaveChanges();

            return Ok(userBank);

        }

        [HttpPost("SendEmail/{toAddress}")]
        public async Task<IActionResult> SendEmail(string toAddress)
        {
            string verificationCode = GenerateVerificationCode();
            string remetente = "ecrz_dev@outlook.com";
            string senha = "Ed_88249483";
            string assunto = "Codigo de verificação";
            string corpo = $"Seu código de verificação é: {verificationCode}";
            string servidorSmtp = "smtp-mail.outlook.com";
            int portaSmtp = 587;

            try
            {
                // Cria uma nova mensagem de e-mail
                MailMessage mensagem = new MailMessage(remetente, toAddress, assunto, corpo);

                // Configura o cliente SMTP
                SmtpClient cliente = new SmtpClient(servidorSmtp, portaSmtp);
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new NetworkCredential(remetente, senha);
                cliente.EnableSsl = true;

                // Envia a mensagem de e-mail
                await cliente.SendMailAsync(mensagem);
                                
                return Ok(verificationCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex);
            }
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userBank = _context.Users.Find(id);

            if (userBank == null)
                return NotFound();

            _context.Users.Remove(userBank);
            _context.SaveChanges();

            return NoContent();
        }



        public static class Crypto
        {
            public static string Encrypt(string plainText)
            {
                string key = "d6a09e6679f3bcc9089f5069cacf5b67c7f31c5d27d5b5d5c5f31dab9ba4a647";
                string iv = "c651cb2a43537f6b9c74069620d6d592";

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using StreamWriter swEncrypt = new StreamWriter(csEncrypt);

                swEncrypt.Write(plainText);

                byte[] encrypted = msEncrypt.ToArray();
                return Convert.ToBase64String(encrypted);
            }

            public static string Decrypt(string cipherText, string key, string iv)
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msDecrypt = new MemoryStream(cipherBytes);
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);

                return srDecrypt.ReadToEnd();
            }
        }



    }
}

