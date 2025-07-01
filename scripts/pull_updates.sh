#!/bin/sh
set -e
REPO_URL="https://github.com/codex/build-an-os-in-assembly-with-cli-and-filesystem.git"
DEST="updates/repo"

if [ ! -d "$DEST/.git" ]; then
    git clone "$REPO_URL" "$DEST"
else
    git -C "$DEST" pull --ff-only
fi
