using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject {
        public Document (string number, EDocumentType type) {
            Number = number;
            Type = type;

            AddNotifications (new Contract ()
                .Requires ()
                .IsTrue (Validate (), "Document.Number", $"{type} inválido"));
        }

        private bool Validate () {
            switch (Type) {
                case EDocumentType.Cnpj:
                    return Number.Length == 14;
                case EDocumentType.Cpf:
                    return IsCPF(Number);
                default:
                    return false;
            }
        }

        public bool IsCPF (string valor) {
            if (string.IsNullOrEmpty (valor))
                return false;

            string cpf = valor;

            int d1, d2;
            int soma = 0;
            string digitado = "";
            string calculado = "";

            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] n = new int[11];

            // Se o tamanho for < 11 entao retorna como inválido
            if (cpf.Length != 11)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cpf) {
                case "00000000000":
                case "11111111111":
                case "2222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            try {
                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32 (cpf.Substring (0, 1));
                n[1] = Convert.ToInt32 (cpf.Substring (1, 1));
                n[2] = Convert.ToInt32 (cpf.Substring (2, 1));
                n[3] = Convert.ToInt32 (cpf.Substring (3, 1));
                n[4] = Convert.ToInt32 (cpf.Substring (4, 1));
                n[5] = Convert.ToInt32 (cpf.Substring (5, 1));
                n[6] = Convert.ToInt32 (cpf.Substring (6, 1));
                n[7] = Convert.ToInt32 (cpf.Substring (7, 1));
                n[8] = Convert.ToInt32 (cpf.Substring (8, 1));
                n[9] = Convert.ToInt32 (cpf.Substring (9, 1));
                n[10] = Convert.ToInt32 (cpf.Substring (10, 1));
            } catch {
                return false;
            }

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound (0); i++)
                soma += (peso1[i] * Convert.ToInt32 (n[i]));

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
                d1 = 0;
            else
                d1 = 11 - resto;

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound (0); i++)
                soma += (peso2[i] * Convert.ToInt32 (n[i]));

            // Pega o resto da divisao
            resto = soma % 11;
            if (resto == 1 || resto == 0)
                d2 = 0;
            else
                d2 = 11 - resto;

            calculado = d1.ToString () + d2.ToString ();
            digitado = n[9].ToString () + n[10].ToString ();

            if (calculado == digitado)
                return (true);
            else
                return (false);
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }
    }
}