#!/bin/sh
set -e
REPO_URL="https://github.com/codex/build-an-os-in-assembly-with-cli-and-filesystem.git"
DIR="external/update_repo"

if [ ! -d "$DIR" ]; then
    git clone "$REPO_URL" "$DIR"
else
    git -C "$DIR" pull
fi

echo "Repository synced at $DIR"
