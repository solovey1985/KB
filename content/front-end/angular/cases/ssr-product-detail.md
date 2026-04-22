# SSR Product Detail with Hydration

## Situation

You are building a product detail page that should:

- show useful content immediately for users and crawlers
- avoid obvious client-side repaint after startup
- support richer interactive widgets after load

## Why this case matters

This case exercises:

- server-side rendering
- hydration
- deferred rendering for heavy UI blocks

## Example

```ts
bootstrapApplication(AppComponent, {
  providers: [
    provideClientHydration(),
  ],
});
```

```html
<article>
  <h1>{{ product.name }}</h1>
  <p>{{ product.description }}</p>

  @defer (on viewport) {
    <app-related-products [productId]="product.id" />
  }
</article>
```

## Practical design notes

- use SSR for content that should appear immediately
- use hydration so the client reuses the server DOM
- defer expensive non-critical widgets instead of delaying the whole page

## Related concepts

- [Server-Side Rendering](../advanced/angular-advanced.concept.md#server-side-rendering)
- [Hydration](../advanced/angular-advanced.concept.md#hydration)
- [@defer Block](../advanced/angular-advanced.concept.md#defer-block)
