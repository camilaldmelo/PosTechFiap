## Estrutura e Funcionamento da API no Kubernetes

Este projeto implementa uma API em um ambiente Kubernetes, garantindo escalabilidade, disponibilidade e eficiência de recursos. A seguir, apresentamos uma descrição detalhada de cada componente da arquitetura Kubernetes para a API e como eles trabalham juntos.

### Deployment (api)

O `Deployment` chamado `api` é responsável por gerenciar os pods da API. Ele define as seguintes configurações:

- **Imagem do Container**: Utilizamos a imagem `docker.io/fabiogmartins13/apitechchallengegrupo01:v10` para os pods.
- **Probes de Liveness**: Implementamos um probe de liveness com uma chamada HTTP para o endpoint `/health` na porta 80. Isso garante que o serviço seja reiniciado automaticamente se parar de responder.
- **Recursos**: Configuramos limites e solicitações de recursos (CPU e memória) para garantir um gerenciamento eficiente dos recursos do cluster.
- **Portas e Variáveis de Ambiente**: O container expõe a porta 80 e utiliza variáveis de ambiente para configurações de banco de dados. A senha do banco de dados é injetada de forma segura usando o `Secret` `postgres-secret`.

### Service (api-service)

O `Service` chamado `api-service` expõe a API dentro e, potencialmente, fora do cluster Kubernetes. Ele utiliza o tipo `LoadBalancer` para distribuir o tráfego de entrada e mapeia a porta 8080 do serviço para a porta 80 do pod.

### HorizontalPodAutoscaler (api-hpa)

O `HorizontalPodAutoscaler` chamado `api-hpa` automatiza o dimensionamento dos pods da API com base na utilização da CPU. As configurações são:

- **Escalamento**: Varia de 1 a 10 réplicas.
- **Meta de Utilização de CPU**: Definida em 50%, o HPA aumenta o número de réplicas quando a utilização média de CPU ultrapassa esse limite.

Com essa configuração, a infraestrutura da API é altamente disponível, escalável e eficiente no uso de recursos. O sistema automaticamente ajusta o número de pods em resposta à carga, garantindo performance e otimização de custos.
