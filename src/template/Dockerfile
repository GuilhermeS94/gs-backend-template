ARG REPO=mcr.microsoft.com/dotnet/aspnet
# FROM $REPO:6.0.3-alpine3.15-amd64
FROM $REPO:7.0.13-alpine3.18-amd64

ENV \
    ASPNETCORE_URLS= \
    DOTNET_SDK_VERSION=7.0.403 \
    # POWERSHELL_DISTRIBUTION_CHANNEL=PSDocker-DotnetSDK-Alpine-3.15
    POWERSHELL_DISTRIBUTION_CHANNEL=PSDocker-DotnetSDK-Alpine-3.18

RUN apk add --no-cache \
    curl \
    icu-libs \
    git

RUN wget -O dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$DOTNET_SDK_VERSION/dotnet-sdk-$DOTNET_SDK_VERSION-linux-musl-x64.tar.gz \
    && dotnet_sha512='' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p usr/share/dotnet \
    && tar -oxzf dotnet.tar.gz -C usr/share/dotnet ./packs ./sdk/ ./sdk-manifests ./templates ./LICENSE.txt ./ThirdPartyNotices.txt \
    && rm dotnet.tar.gz \
    && dotnet help

RUN apk update
RUN apk upgrade
RUN rm -rf /var/cache/apk/*
# ADD URL_CERTIFICATE
# COPY ./ca_bundle.crt /usr/local/share/ca-certificates/ca_bundle.crt
# RUN update-ca-certificates

ENV ASPNETCORE_URLS=http://*:3000
EXPOSE 3000

WORKDIR /app

COPY ./app ./

ENTRYPOINT ["dotnet", "GS.Backend.NomeAplicacao.Api.dll"]