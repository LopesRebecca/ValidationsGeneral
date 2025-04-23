
# ValidationsGeneral

## Descrição
O **ValidationsGeneral** é uma biblioteca desenvolvida em .NET 8 que 
fornece um conjunto de validações genéricas para facilitar o desenvolvimento de aplicações.
Ele foi projetado para ser simples, reutilizável e eficiente, 
ajudando a reduzir a duplicação de código e a melhorar a consistência das validações em
diferentes projetos.

## Validações Disponíveis
1. **Validações de Identidade**
    - **Validação de CPF**: Verifica se um CPF é válido.
    - **Validação de CNPJ**: Verifica se um CNPJ é válido.

2. **Validações Financeiras**
    - **Validação de Cartão de Crédito**: Verifica se o número do cartão é válido.

3. **Validações de Contato**
    - **Validação de Telefone**: Verifica se o número de telefone é válido.
    - **Validação de Email**: Verifica se um endereço de email é válido.
    - **Validação de IP**: Verifica se o IP é válido.
    - **Validação de URL**: Verifica se a URL está de acordo com as regras.

4. **Validações de Localização**
    - **Validação de CEP**: Verifica se o CEP é válido.
    - **Validação de Código do País**: Verifica se o código está válido.
    - **Validação de TimeZone**: Verifica se a Timezone é válida.

5. **Validações de Data**
    - **Validação de Data no formato ISO**: Verifica se a data está no formato correto.
    - **Validação de Data no formato personalizado**: Verifica se a data está no formato correto.
    - **Validação de Intervalo de Datas**: Verifica se as datas estão no intervalo correto.
    - **Validação de Intervalo de Datas com um formato personalizado**: Verifica se as datas estão no intervalo correto e com o formato personalizado.
    - **Validação de Idade**: Verifica se a idade está de acordo com a regra cadastrada.

## Como Usar

### 1. Instale o pacote NuGet
O pacote NuGet está disponível no seguinte link: [ValidationsGeneral](https://www.nuget.org/packages/ValidationsGeneral/).

### 2. Chame o pacote

```bash
dotnet add package ValidationsGeneral
```

### 3. Exemplos de Validação

```csharp
using ValidationsGeneral;

// Validação de CPF
var cpfValido = DocumentValidator.IsValidCpf("123.456.789-09");
Console.WriteLine($"CPF válido: {cpfValido}");

// Validação de CNPJ
var cnpjValido = DocumentValidator.IsValidCnpj("12.345.678/0001-95");
Console.WriteLine($"CNPJ válido: {cnpjValido}");

// Validação de Cartão de Crédito
var cartaoValido = CreditCardValidator.IsValid("4111 1111 1111 1111");
Console.WriteLine($"Cartão válido: {cartaoValido}");

// Validação de Email
var emailValido = ContactValidator.IsValidEmail("exemplo@email.com");
Console.WriteLine($"Email válido: {emailValido}");

// Validação de CEP
var cepValido = LocationValidator.IsValidCep("01001-000");
Console.WriteLine($"CEP válido: {cepValido}");

// Validação com mensagens de erro internacionalizadas
var resultado = DocumentValidator.ValidateCpf("12345678900", culture: "pt-BR");
if (!resultado.IsValid)
{
    Console.WriteLine(resultado.ErrorMessage); // Ex: "CPF inválido"
}
```

### 4. Configuração de Injeção de Dependência (DI)

Para integrar a biblioteca **ValidationsGeneral** com Injeção de Dependência, adicione o seguinte código no seu `Startup.cs` ou `Program.cs` (dependendo da versão do .NET que você estiver usando):

```csharp
using Microsoft.Extensions.DependencyInjection;
using ValidationsGeneral.Extensions;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Registra as estratégias de validação
        services.AddValidationStrategies();
        
        // Outros serviços...
    }
}
```

Agora, você pode injetar as estratégias de validação onde for necessário. Por exemplo, se você precisar usar uma estratégia de validação de CPF em um serviço:

```csharp
public class MeuServico
{
    private readonly CpfValidatorStrategy _cpfValidator;

    public MeuServico(CpfValidatorStrategy cpfValidator)
    {
        _cpfValidator = cpfValidator;
    }

    public bool ValidarCpf(string cpf)
    {
        return _cpfValidator.Validate(cpf);
    }
}
```

### 5. Usando o ValidatorFactory

A classe **ValidatorFactory** facilita a criação dinâmica de validadores com base no tipo de documento. Você pode usá-la quando precisar de uma abordagem mais flexível para validar documentos diferentes sem precisar instanciar manualmente os validadores.

Exemplo de uso:

```csharp
using ValidationsGeneral.Factory;

public class DocumentService
{
    public bool ValidarDocumento(DocumentType tipo, string documento)
    {
        // Cria o validador apropriado para o tipo de documento
        var validator = ValidatorFactory.Create(tipo);
        
        // Valida o documento
        return validator.Validate(documento);
    }
}
```

### 6. Método de Extensão IsValid

A classe **ValidationExtensions** permite que você use a validação de forma mais fluída com a adição de um método de extensão `IsValid()`. Agora, você pode validar entradas de forma mais concisa sem precisar acessar diretamente o método `Validate` e o campo `IsValid`.

Exemplo de uso:

```csharp
var cpfValidator = new CpfValidatorStrategy();

if (cpfValidator.IsValid("123.456.789-09"))
{
    Console.WriteLine("CPF válido!");
}
else
{
    Console.WriteLine("CPF inválido!");
}
```

### Requisitos

- **.NET 8** ou superior.

## 🛣 Roadmap

- [x] Validação de CPF
- [x] Validação de CNPJ
- [x] Validação de Cartão de Crédito
- [x] Validações de Contato (Email, Telefone, IP, URL)
- [x] Validações de Localização (CEP, Código do País, TimeZone)
- [x] Validações de Datas e Idade
- [x] Integração com SonarQube
- [ ] Validação de NIF (Portugal)
- [ ] Validação de Passaporte
- [ ] Validação de SSN (EUA)
- [ ] Suporte completo à internacionalização de mensagens de erro
- [ ] Publicação automatizada via CI/CD

## 🤝 Contribuindo

Contribuições são muito bem-vindas! Para contribuir:

1. Faça um fork do repositório.
2. Crie uma branch com a sua feature ou correção:
   ```bash
   git checkout -b minha-feature
   ```
3. Commit suas alterações:
   ```bash
   git commit -m "Adiciona nova validação X"
   ```
4. Envie um pull request!

Sinta-se à vontade para abrir uma *issue* caso tenha sugestões, dúvidas ou encontre algum bug. 💬