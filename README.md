# Feriados

É uma biblioteca gratuita e de código aberto, que disponibiliza feriados nacionais, estaduais e municipais, conforme o calendário gregoriano.

![CI - Build](https://github.com/marcos-cruz/Feriados/workflows/CI%20-%20Build/badge.svg)
![CI - Tests](https://github.com/marcos-cruz/Feriados/workflows/CI%20-%20Tests/badge.svg)

# Índice

- [Objetivo](#objetivo)
- [Recursos](#recursos)
- [Feriados Suportados](#feriados-suportados)
  * [Feriados Nacionais](#feriados-nacionais)
  * [Feriados Estaduais](#feriados-estaduais)
  * [Feriados Municipais](#feriados-municipais)
- [Instalação](#instalação)
- [Utilização](#utilização)
- [Contrato de Contribuição e Licença](#contrato-de-contribuição-e-licença)
- [Licença](#licença)
- [Sobre Bigai](#sobre-bigai)

# Objetivo

Disponibilizar uma biblioteca dotnet C#, que permita a consultar feriados Nacionais, Estaduais e Municipais, no tempo presente, passado ou futuro.

# Recursos

* Obter uma lista com todos os feriados de um ano.
* Obter uma lista somente os feriados nacionais de um ano.
* Obter uma lista somente com os feriados estaduais de um estado e ano específicos.
* Obter uma lista somente com os feriados municipais de um município e ano específicos.
* Obter uma lista com os feriados que ocorrem em um mês específico.
* Obter os feriados que podem ocorrer em uma data específica.

# Feriados Suportados

São suportados os Feriados Nacionais, Estaduais e Municipais.

## Feriados Nacionais

| Dia | Mês | Descrição |
|-|-|-|
| 01 | 01 | Confraternização Universal |
| 21 | 04 | Tiradentes |
| 01 | 05 | Dia do Trabalho |
| 07 | 09 | Independência do Brasil |
| 12 | 10 | Nossa Senhora Aparecida |
| 02 | 11 | Finados |
| 15 | 11 | Proclamação da República |
| 25 | 12 | Natal |
| *** |    | Segunda-feira de Carnaval |
| *** |    | Terça-feira de Carnaval |
| *** |    | Quarta-feira de Cinzas |
| *** |    | Sexta-Feira Santa |
| *** |    | Domingo de Páscoa |
| *** |    | Corpus Christi |

Legenda: *** Feriados nacionais com datas móveis

## Feriados Estaduais

| Dia | Mês | UF | Descrição |
|-|-|-|-|
| 23 | 1 | AC | Dia do Evangélico |
| 15 | 6 | AC | Aniversário do Acre |
| 5 | 9 | AC | Dia da Amazônia |
| 17 | 11 | AC | Assinatura do Tratado de Petrópolis |
| 24 | 6 | AL | São João |
| 29 | 6 | AL | São Pedro |
| 16 | 9 | AL | Emancipação Política de Alagoas |
| 20 | 11 | AL | Consciência Negra |
| 5 | 9 | AM | Elevação do Amazonas à categoria de Província |
| 20 | 11 | AM | Consciência Negra |
| 8 | 12 | AM | Dia de Nossa Senhora da Conceição |
| 19 | 3 | AP | Dia de São José |
| 25 | 7 | AP | São Tiago |
| 5 | 10 | AP | Criação do Estado do Amapá |
| 20 | 11 | AP | Consciência Negra |
| 2 | 7 | BA | Independência da Bahia |
| 19 | 3 | CE | Dia de São José |
| 25 | 3 | CE | Data Magna do Ceará |
| 21 | 4 | DF | Fundação de Brasília |
| 30 | 11 | DF | Dia do Evangélico |
| 28 | 10 | ES | Dia do Servidor Público |
| 28 | 10 | GO | Dia do Servidor Público |
| 28 | 7 | MA | Adesão do Maranhão à Independência |
| 8 | 12 | MA | Nossa Senhora da Conceição |
| 21 | 4 | MG | Data Magna de Minas Gerais |
| 11 | 10 | MS | Criação do Estado do Mato Grosso do Sul |
| 20 | 11 | MT | Consciência Negra |
| 15 | 8 | PA | Adesão do Grão-Pará à Independência |
| 5 | 8 | PB | Fundação do Estado da Paraíba |
| 24 | 6 | PE | São João |
| 13 | 3 | PI | Dia da Batalha do Jenipapo |
| 19 | 10 | PI | Dia do Piauí |
| 23 | 4 | RJ | Dia de São Jorge |
| 28 | 10 | RJ | Dia do Funcionário Público |
| 20 | 11 | RJ | Zumbi dos Palmares |
| 8 | 12 | RJ | Dia de Nossa Senhora da Conceição |
| 29 | 6 | RN | Dia de São Pedro |
| 3 | 10 | RN | Mártires de Cunhaú e Uruaçuu |
| 4 | 1 | RO | Criação do Estado de Rondônia |
| 18 | 6 | RO | Dia do Evangélico |
| 5 | 10 | RR | Criação do Estado de Roraima |
| 20 | 9 | RS | Revolução Farroupilha |
| 11 | 8 | SC | Criação da Capitania |
| 8 | 7 | SE | Autonomia Política de Sergipe |
| 9 | 7 | SP |  Revolução Constitucionalista de 1932 |
| 1 | 1 | TO | Instalação de Tocantins |
| 8 | 9 | TO | Nossa Senhora da Natividade |
| 5 | 10 | TO | Criação de Tocantins |

## Feriados Municipais

Todas as datas comemorativas de Aniversário de todos os municípios do Brasil.

# Instalação

TODO: Disponibilizar Nuget Package

# Utilização

```csharp
//
// Retorna uma instância de feriado para o ano solicitado.
//
var feriado = Feriado.Factory(anoDoFeriado);

//
// Retorna uma lista ordenada com todos os feriados
//
var feriados = feriado.Feriados.ToList();

//
// Retorna uma lista ordenada com todos os feriados nacionais
//
var feriadosNacionais = feriado.FeriadosNacionais("BR").ToList();

//
// Retorna uma lista ordenada com todos os feriados do município de São Paulo
//
var codigoIbge = "3550308";
var feriadosMunicipais = feriado.FeriadosMunicipais(codigoIbge).ToList();

//
// Retorna uma lista com os feriados para uma data específica
//
var dataDoFeriado = new DateTime(2076, 04, 19);
var feriadosNaData = feriado.EhFeriado(dataDoFeriado).ToList();

```

# Contrato de Contribuição e Licença

Se você deseja contribuir com código para este projeto, saiba que implicitamente está permitindo que seu código seja distribuído sob a licença GPL-3.0 License. Você também assume a responsabilidade de:

1. Todo o código fonte fornecido, é obra original sua, ou;
2. Atribuir corretamente a fonte de sua origem e licença.

# Licença

Feriados - Copyright (c) 2020 - Marcos Cruz, Bigai Consultoria Software (GNU General Public License v3.0)

# Sobre Bigai

Bigai é um apelido carinhoso da minha mãe, que resolvi adotar em minhas iniciativas de empreendedorismo.
