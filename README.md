
# Tech Challenge - Pós-Tech SOAT - FIAP - Grupo 01

Este é o projeto desenvolvido durante a fase I do curso de pós-graduação em arquitetura de software da FIAP - 3SOAT



## Membros:
- [Camila Lemos de Melo - RM 352359]()
- [Christian Soares Santos - RM 351509](https://www.linkedin.com/in/christian-soares-93250a170/)
- [Fábio Guimarães Martins - RM 351825](https://www.linkedin.com/in/fabio-martins-2021)
- [Josué Batista Cruz Júnior - RM 352045](https://www.linkedin.com/in/josuejuniorjf/)


## Documentação

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