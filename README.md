# My Website [![Build and scan](https://github.com/CorruptComputer/home/actions/workflows/build-scan.yml/badge.svg)](https://github.com/CorruptComputer/home/actions/workflows/build-scan.yml)

## Built With
- [Jekyll](https://jekyllrb.com/) as the templating engine
- [Utterances](https://github.com/utterance/utterances) for comments on blog posts
- [Cloudflare](https://www.cloudflare.com) for DNS and hosting
- [highlight.js](https://github.com/highlightjs/highlight.js) for syntax highlighting in code blocks

## Notes
- Deployed with Ruby 2.7, as this is the latest currently [supported by Cloudflare Pages](https://developers.cloudflare.com/pages/platform/build-configuration#language-support-and-tools).
- Worked around Cloudflare Pages deployment issue with change to Gemfile
  - https://community.cloudflare.com/t/deployment-failing-rubygems-version/446483
  - https://github.com/jekyll/jekyll/pull/9225