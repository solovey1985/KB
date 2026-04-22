# HTML Media and Responsive Content

HTML5 made media a first-class part of the platform.

## Images

Basic images use `img`, but responsive content often needs `srcset`, `sizes`, or `picture`.

```html
<img
  src="hero-800.jpg"
  srcset="hero-400.jpg 400w, hero-800.jpg 800w, hero-1200.jpg 1200w"
  sizes="(max-width: 600px) 100vw, 800px"
  alt="Team working together in the studio"
/>
```

## Audio and video

```html
<video controls preload="metadata" width="640">
  <source src="intro.mp4" type="video/mp4" />
  <track kind="captions" src="intro.en.vtt" srclang="en" label="English" />
</video>
```

Important concerns:

- controls
- captions and subtitles
- preload behavior
- autoplay restrictions

## Embeds

`iframe` is common for embedded third-party experiences such as maps, videos, or documents.

Use it carefully because embedded content can affect performance, security, and user experience.

## Interview reminders

- mention captions when discussing video accessibility
- explain `srcset` as responsive image selection help for the browser
- mention `iframe` trade-offs, not only its syntax
