#!/bin/sh
# If the OpenShift‑assigned UID doesn’t exist, add a throw‑away passwd entry
if ! id -un >/dev/null 2>&1; then
  if [ -w /etc/passwd ]; then
    echo "anon:x:$(id -u):0:Anon:${HOME:-/tmp}:/sbin/nologin" >>/etc/passwd
  fi
fi
exec "$@"
