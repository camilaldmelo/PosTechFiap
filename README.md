
# Tech Challenge - Pós-Tech SOAT - FIAP - Grupo 01

Este é o projeto desenvolvido durante a fase I do curso de pós-graduação em arquitetura de software da FIAP - 3SOAT



## Membros:
- [Camila Lemos de Melo - RM 352359]()
- [Christian Soares Santos - RM 351509](https://www.linkedin.com/in/christian-soares-93250a170/)
- [Fábio Guimarães Martins - RM 351825](https://www.linkedin.com/in/fabio-martins-2021)
- [Josué Batista Cruz Júnior - RM 352045](https://www.linkedin.com/in/josuejuniorjf/)


## Documentação - Entregável 01 - Fase 1

### Problema atual - AS IS
Uma lanchonete de bairro, está expandindo devido ao seu grande sucesso. Porém encontra grande dificuldade para gerenciar os pedidos manualmente. Em alguns momentos o atendimento aos clientes é caótico e confuso. O atendente anota o pedido em um papel e entrega à cozinha, mas não se tem nenhuma garantina que o pedido será preparado corretamente.

Pode ocorrer confusão entre os atendentes e a cozinha, resultando em atrasos na preparação e entrega dos pedidos. Os pedidos podem ser perdidos, mal interpretados ou esquecidos, gerando insatisfação dos clientes.

![Fluxo atual](https://cdn.discordapp.com/attachments/1144408943993573376/1151336340416372817/Fluxo_de_Atendimento_Atual_Tue_Sep_12.egn_2023-09-13.png?ex=65143546&is=6512e3c6&hm=9517d56fb73ae2f3ffcfe2568f96efaba20798194e1c285bbf2243355397663c&)

### Melhoria no processo - TO BE
Um sistema de controle de pedidos é essencial para garantir que a lanchonete possa atender os clientes de maneira eficiente, gerenciando seus pedidos e estoques de forma adequada. 
Para solucionar o problema, a lanchonete quer investir em um sistema de autoatendimento de fast food.

![Fluxo melhorado](https://cdn.discordapp.com/attachments/1144408943993573376/1151336336746364969/Fluxo_de_Autoatendimento_Novo_2023-09-13.png?ex=65143545&is=6512e3c5&hm=79323abadedd9cdb0a17b0a7704ad039ef09831da420a7d9f6cbb575349de835&)

#### Storytelling do processo de autoatendimento para pedidos
"O processo para realização de pedido começa com o cliente se aproximando o toten de autoatendimento e iniciando o processo. O autoatendimento pergunta os dados (CPF, Nome e e-mail) do cliente. O cliente tem a opção de pular essa etapa de identificação. Na segunda tela do autoatendimento é exibido o menu, com as opções para pedido, como bebidas, lanches, acompanhamentos e sobremesas, onde o cliente pode interagir selecionando o que deseja, bem como as quantidades. Durante essa interação o autoatendimento irá calcular o valor do pedido, conforme as escolhas do cliente. Ao termino dessa etapa, o autoatendimento irá gerar o QRCode para pagamento e ficará aguardando que o pagamento seja efetuado por até 3 minutos. O cliente deverá realizar o pagamento do QRCode utilizando seu celular. Com a identificação do pagamento o autoatendimento irá imprimir o ticket com as informações do pedido para o cliente e enviará a notificação de pedido recebido para que a cozinha inicie a preparação. Quando a cozinha iniciar a preparação do pedido, deverá informar no sistema disponível na cozinha, que o pedido está em preparação. Com o termino na preparação do pedido, a cozinha deverá novamente informar no sistema disponível na cozinha, que o pedido está pronto. Nesse momento será notificado ao cliente que o seu pedido está pronto e disponível para ser entregue. O cliente se direciona ao balcão, apresentando o ticket com as informações do pedido ao atendente que entrega o pedido ao cliente. O atendente notifica no sistema que o pedido está finalizado."


### Dicionário linguagem ubíqua
 - Cliente - Ator que opera a ação de fazer um pedido;
 - Atendente - Ator que realiza o atendimento junto com o cliente para realização do pedido;
 - Cozinha - Ator que recebe o pedido e realiza a produção do produto final e entrega interagindo com o autoatendimento;
 - Autoatendimento - Plataforma sistêmica que realiza o envio do pedido para a cozinha e atualização da situação da mesma. Interage com o atendente e cozinha;
- Pedido - Solicitação da realização do produto final contendo detalhes de acompanhamentos/ possíveis alterações e bebidas;
 - Ticket - Comprovante da realização do pedido e pagamento do mesmo entre o cliente e atendente;
 - Menu - Lista com itens para composição do produto final com opções de lanches, acompanhamentos e bebidas;

### Mapa de contextos
![Mapa de contextos](https://cdn.discordapp.com/attachments/1144408943993573376/1156358369611882556/image.png?ex=6514ae27&is=65135ca7&hm=1a0b765bed1ba45af69d0f8d0233aaae11f9953f3c67d1c050e78c1770b66cea&)

### Event Storming
O Event Storming foi realizado na ferramenta Miro e está disponível no [aqui](https://miro.com/app/board/uXjVMqYSzbg=/?share_link_id=48933981551)

---

## Endpoints disponíveis - Entregável 02 - Fase 1

Foram disponibilizados os metodos de Cadastro, Remoção, Busca e Atualização dos seguintes recursos:
 - Acompanhamento
 - Categoria
 - Cliente
 - Pedido
 - Produto

É possível acessar a documentação da API via Swagger ou ReDoc.

[Baixe o Postman Collection](Grupo01.postman_collection.json)

## APIs
Abaixo serão listados apenas os metodos solicitados no entregável 02 do TechChallenge Fase 1. Os demais metodos poderão ser consultados na documentação da API via Swagger/ReDoc ou utilizando o Postman Collection.
### I. Cadastro do Cliente
```bash
POST http://localhost:8080/Cliente

{
  "cpf": "98765432110",
  "nome": "Cristiano Ronaldo",
  "email": "cr7@email.com"
}
```
### II. Identificação do Cliente via CPF
```bash
GET http://localhost:8080/Cliente/cpf/12345678901
```
### III. Criar, editar e remover produto
```bash
POST http://localhost:8080/Produto
{
  "nome": "Água com Gás",
  "descricao": "Água mineral natural",
  "preco": 3.99,
  "urlImagem": "https://example.com/aguagas.jpg",
  "idCategoria": 3
}
```
```bash
PUT http://localhost:8080/Produto/1
{
    "nome": "Hambúrguer",
    "descricao": "Hambúrguer delicioso",
    "preco": 15.99,
    "urlImagem": "https://example.com/imagem1.jpg",
    "idCategoria": 1,
    "categoria": "Lanche"
}
```
```bash
DELETE http://localhost:8080/Produto/30
```

### IV. Buscar produtos por categoria
```bash
GET http://localhost:8080/Produto/Categoria/2
```

### V. Fake checkout, apenas enviar os produtos escolhidos para a fila
```bash
POST http://localhost:8080/Pedido/PedidoStatus?idPedido=1&idStatus=2
```

### VI. Listar os pedidos
```bash
GET http://localhost:8080/Pedido/Status/1
```
---

## Como Clonar e Subir a Aplicação com Docker Compose - Entregável 03 - Fase 1

Este guia fornecerá instruções passo a passo sobre como clonar e executar a aplicação .NET 7.0 com um banco de dados PostgreSQL usando Docker Compose. Certifique-se de ter o Docker e o Docker Compose instalados em seu sistema antes de prosseguir.

### Passo 1: Clone o Repositório

Para começar, você precisa clonar o repositório do GitHub para sua máquina local. Abra o terminal e execute o seguinte comando:

```bash
git clone https://github.com/camilaldmelo/PosTechFiap.git
```

### Passo 2: Navegue até o Diretório do Projeto
Após clonar o repositório, navegue até o diretório do projeto usando o seguinte comando:
```bash
cd PosTechFiap
```
### Passo 3: Inicialize os Contêineres Docker
A aplicação depende de dois contêineres Docker: um para o PostgreSQL e outro para a API .NET. Você pode iniciar ambos usando o Docker Compose. Certifique-se de estar no diretório raiz do projeto (onde está o arquivo docker-compose.yml) e execute o seguinte comando:
```bash
docker-compose up -d --build
```
Isso iniciará os contêineres em segundo plano (-d). O Docker Compose lerá as configurações do docker-compose.yml e criará os contêineres necessários.

### Passo 4: Aguarde a Inicialização
Aguarde alguns momentos enquanto o Docker Compose cria e inicializa os contêineres. Isso pode levar algum tempo na primeira execução, pois ele precisará baixar as imagens Docker necessárias.

### Passo 5: Acesse a Aplicação
Após a inicialização bem-sucedida, você pode acessar a aplicação em seu navegador ou usando ferramentas como o curl. A API estará disponível na porta 8080.
- Acesse a API em http://localhost:8080/swagger ou http://localhost:8080/api-docs
- O PostgreSQL estará disponível em localhost na porta padrão 5432.

### Passo 6: Parar e Remover Contêineres (Opcional)
Para parar e remover os contêineres, você pode executar o seguinte comando no diretório do projeto:
```bash
docker-compose down
```
Isso encerrará os contêineres e os removerá. Você pode executar docker-compose up -d novamente para iniciar os contêineres quando desejar.

## Como executar uma arquitetura em Kubernetes - Entregavel 2 - Fase 2

### Passo 1: Clone o Repositório

Para começar, você precisa clonar o repositório do GitHub para sua máquina local. Abra o terminal e execute o seguinte comando:

```bash
git clone https://github.com/camilaldmelo/PosTechFiap.git
```

### Passo 2: Navegue até o Diretório do Projeto
Após clonar o repositório, navegue até o diretório do projeto usando o seguinte comando:
```bash
cd PosTechFiap/Kubernetes
```
### Passo 3: Navegue até o Diretório da api
Navegue até o diretório da api usando o seguinte comando:
```bash
cd api
```

### Passo 4: Execute os comandos na ordem:
Execute usando o seguinte comando:
```bash
kubectl apply -f "1 - api-deployment.yml"
kubectl apply -f "2 - api-service.yml"
kubectl apply -f "3 - api-hpa.yml"
```

### Passo 5: Navegue até o Diretório do db
Navegue até o diretório da api usando o seguinte comando:
```bash
cd ..
cd db
```

### Passo 6: Execute os comandos na ordem:
Execute usando o seguinte comando:
```bash
kubectl apply -f "1 - postgres-secret.yml"
kubectl apply -f "2 - init-sql-script-configmap.yml"
kubectl apply -f "3 - postgres-pvc.yml"
kubectl apply -f "4 - postgres-statefulset.yml"
kubectl apply -f "5 - postgres-service.yml"
kubectl apply -f "6 - postgres-init-job.yml"
```

### Deletar:
Para deletar, execute o seguinte comando dentro do diretorio do projeto:
```bash
kubectl delete all --all
```

## Entregavel 3 - Fase 2
### Desenho da arquitetura
![Desenho da arquitetura](https://cdn.discordapp.com/attachments/1144408943993573376/1198276440500732084/image.png?ex=65be50e6&is=65abdbe6&hm=2744212da50d87b7e29d16daa5d53ab958a2449c3fb3ba51473a807c889a1cbe&)