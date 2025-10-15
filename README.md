# Projeto Analise Credito

O projeto visa demonstrar a criação de uma biblioteca para abstração do consumo do serviço de mensageria RabbitMQ.

Foi pensado em uma estrutura de monorepo dividida entre as pastas de backend e frontend:
# Backend
## Rabbit Hole
Na pasta **RabbitHole** em /backend encontra-se a biblioteca de integração com o serviço RabbitMQ.
Com o foco em manejo de retentativas, como dependência foi acrescentada a biblioteca **Polly** na versão **8.6.4**, que permite a api retentar a conexão algumas vezes antes de prosseguir. 

O pacote foi compilado usando o comando: 
> dotnet pack --configuration Release

Depois de compilado, o arquivo .nupkg adicionado ao repositorio local do nuget com o comando
> nuget sources Add -Name "NomeDaFonte" -Source "Caminho\Para\Pasta\Local"

Isso permite ao nuget entender pastas locais mapeadas como repositorios iguais ao repositorio remoto listados nas IDEs como Visual Studio ou Rider

O projeto foi pensado usando o padrão de arquitetura CQRS juntamente com DDD. Onde cada entidade possui um serviço de dominio (estrutura genérica) injetado via container de dependência, o qual cada camada da aplicação possui a sua.

# Frontend.
Foi criado uma aplicação react, usando o **[Vite](https://vite.dev/)** como ferramenta de compilador para agilizar o processo de desenvolvimento. A aplicação conta também com o **[Axios](https://vite.dev/)** para consumo simplificado da api, o **[React Hook Forms](https://react-hook-form.com/)** para gestão de formulario e  o **[Zustand + Immer](https://zustand-demo.pmnd.rs/)** para gestão de estado dos objetos.
