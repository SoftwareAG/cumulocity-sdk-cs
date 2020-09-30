#!/usr/bin/env bash
if [ -z "$TPP_FETCHER_URL" ]; then TPP_FETCHER_URL="http://172.30.0.129:8083"; fi


if [[ -e Tpp.toml ]] ; then
  curl -sS -o tpp-scanner ${TPP_FETCHER_URL}/tpp-scanner \
    && chmod +x tpp-scanner \
    && ./tpp-scanner -tpp-fetcher-url ${TPP_FETCHER_URL} \
    && rm tpp-scanner
  exit 0
fi
