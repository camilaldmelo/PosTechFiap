FROM postgres:latest

# Instalação do pacote de locales
RUN apt-get update && apt-get install -y locales && rm -rf /var/lib/apt/lists/* \
    && localedef -i pt_BR -c -f UTF-8 -A /usr/share/locale/locale.alias pt_BR.UTF-8

# Define o locale padrão
ENV LANG pt_BR.utf8

# Copie o script SQL para a pasta /docker-entrypoint-initdb.d/
COPY init.sql /docker-entrypoint-initdb.d/
