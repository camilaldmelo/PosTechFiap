## Estrutura e Funcionamento do Banco de Dados no Kubernetes

Este projeto utiliza Kubernetes para orquestrar um banco de dados PostgreSQL, garantindo escalabilidade, persistência e segurança. Abaixo, segue uma descrição detalhada de cada componente da nossa arquitetura Kubernetes e como eles interagem para fornecer uma solução robusta de banco de dados.

### Secret (postgres-secret)

O `Secret` chamado `postgres-secret` é usado para armazenar informações confidenciais, mais especificamente a senha do banco de dados PostgreSQL. Essa abordagem melhora a segurança, mantendo dados sensíveis fora do código e dos scripts de configuração. O valor da senha é armazenado em formato base64.

### ConfigMap (init-sql-script)

Utilizamos um `ConfigMap`, identificado como `init-sql-script`, para armazenar o script de inicialização do banco de dados (`init.sql`). Este script contém instruções SQL para criar tabelas necessárias para a aplicação (como `TBL_CLIENTE`, `TBL_CATEGORIA`, etc.) e inserir dados iniciais. Este método permite uma fácil manutenção e atualização do script sem necessidade de reconstruir imagens ou reimplantar pods.

### PersistentVolumeClaim (postgres-pvc)

O `PersistentVolumeClaim` (PVC) `postgres-pvc` é crucial para gerenciar o armazenamento de dados do banco de dados de forma persistente. Solicitamos 10Gi de armazenamento, garantindo que os dados do banco de dados não sejam perdidos mesmo que os pods sejam reiniciados ou realocados. Isso é essencial para manter a integridade dos dados em um ambiente de produção.

### StatefulSet (postgres)

O `StatefulSet` denominado `postgres` gerencia os pods do banco de dados PostgreSQL. Optamos por utilizar a imagem `postgres:latest` e configuramos o pod para expor a porta 5432. Implementamos sondas de liveness e readiness para monitorar a saúde e a disponibilidade do serviço. A senha do banco de dados é seguramente injetada no pod a partir do `Secret` criado. Além disso, o armazenamento persistente é garantido através do montagem do PVC. Este setup garante que o banco de dados seja robusto e resiliente a falhas.

### Service (postgres)

O `Service` chamado `postgres` é responsável por expor o banco de dados PostgreSQL dentro do cluster Kubernetes. Ele permite a comunicação com o banco de dados através da porta 5432, facilitando a conexão entre os serviços e o banco de dados de forma confiável e segura.

### Job (postgres-init-job)

Por fim, o `Job` `postgres-init-job` é utilizado para inicializar o banco de dados. Ele executa o script SQL contido no `ConfigMap` após um atraso inicial de 30 segundos, tempo este para garantir que o banco de dados esteja operacional. O script é executado automaticamente, configurando as tabelas e inserindo os dados iniciais necessários para o funcionamento da aplicação.

Em resumo, esta arquitetura do Kubernetes fornece uma solução de banco de dados PostgreSQL segura, persistente e altamente disponível, ideal para aplicações empresariais que exigem confiabilidade e escalabilidade.
