# Syntha API Guide: Using the /resources, /find, and /ask Endpoints

## Introduction

Syntha's API allows developers to ingest data and retrieve information using AI-powered search and Q&A. Once your data is uploaded into a Knowledge Box (Syntha's data container) and processed, you can use the /find endpoint to search for relevant data or the /ask endpoint to ask questions and get generative answers. Before using these endpoints, obtain an API key for your Knowledge Box (from the Syntha dashboard) and include it in your requests for authentication.

All examples below assume you have:

## Authentication

*   Knowledge Box ID (KB\_ID) and Zone (region) for your Knowledge Box (e.g., "europe-1").
    
*   API Key with appropriate role (Reader/Writer/Manager) for that Knowledge Box.
    

### Header:

Use the API key in the X-SYNTHA-SERVICEACCOUNT header as a Bearer token for all requests

For example:

```
X-SYNTHA-SERVICEACCOUNT: Bearer <YOUR_API_KEY>
```

Below, we cover each endpoint's purpose, required headers, example usage in JavaScript, and best practices.

## /resources Endpoint

### Purpose:

Manage and ingest Resources in your Knowledge Box. A Resource in Syntha is any piece of data you upload – documents, images, videos, audio, etc., along with its metadata). Use the /resources endpoint to list existing resources or create new ones (ingest data).

### Endpoint URL:

```
https://<ZONE>.syntha.cloud/api/v1/kb/<KB_ID>/resources
```

### Methods:

*   **GET** – List all resources in the Knowledge Box.
    
*   **POST** – Create a new resource (ingest new data).
    

### Required Headers:

*   `X-SYNTHA-SERVICEACCOUNT: Bearer <YOUR_API_KEY>` – API key for authentication.
    
*   `Content-Type: application/json` – for POST requests (sending JSON body).
    

### Use Cases:

Listing resources: retrieve an overview of all data items (and their metadata) stored in your Knowledge Box.

### Example - Listing Resources:

```javascript
const KB_ID  = '<YOUR_KB_ID>';
const ZONE   = '<YOUR_ZONE>';        // e.g., "europe-1"
const API_KEY = '<YOUR_API_KEY>';

fetch(`https://${ZONE}.syntha.cloud/api/v1/kb/${KB_ID}/resources`, {
  headers: {
    'X-SYNTHA-SERVICEACCOUNT': `Bearer ${API_KEY}`
  }
})
.then(res => res.json())
.then(data => {
  console.log('Resources:', data);
  // Each item in data.resources will include its id, title, slug, metadata, etc.
})
.catch(err => console.error('Error fetching resources:', err));
```

### Example - Creating a Resource:

```javascript
const newResource = {
  title: "Sample Document",
  slug: "sample-document",  // optional slug for easy reference
  texts: {
    "content": {
      format: "PLAIN",
      body: "This is the content of the document to index."
    }
  }
};

fetch(`https://${ZONE}.syntha.cloud/api/v1/kb/${KB_ID}/resources`, {
  method: 'POST',
  headers: {
    'X-SYNTHA-SERVICEACCOUNT': `Bearer ${API_KEY}`,
    'Content-Type': 'application/json'
  },
  body: JSON.stringify(newResource)
})
.then(res => res.json())
```

### Example Response:

### Rate limits:

Ingesting many resources quickly can trigger ingestion back-pressure. Syntha imposes limits to prevent overload – if you send data too fast, the API may return HTTP 429 (Too Many Requests) with a try\_after timestamp in the response. This indicates you should pause and retry after that time. The default overall rate limit is 2400 requests/minute for an account. Handle 429 responses by implementing a retry with exponential backoff to gracefully manage rate limits.

## /find Endpoint

### Purpose:

Perform a search query to find relevant content in your Knowledge Box. The /find endpoint combines semantic search (meaning-based) and keyword/full-text search to retrieve the most relevant data (e.g., paragraphs or documents) for your query. This is the core search API to build search functionality in your application.

### Endpoint URL:

```
https://<ZONE>.syntha.cloud/api/v1/kb/<KB_ID>/find
```

### Method:

You can use GET or POST for searches:

*   **GET** – Simplest usage, pass the query as a URL parameter. e.g. `GET .../find?query=How to ride a bike.`
    
*   **POST** – Allows a JSON request body for advanced searches (multiple parameters like filters, specific features, paging, etc.).
    

### Required Headers:

*   `X-SYNTHA-SERVICEACCOUNT: Bearer <YOUR_API_KEY>` – API key for authentication.
    
*   `Content-Type: application/json` – for POST requests (sending JSON body).
    

### Parameters:

*   `autofilter`: boolean, if true, Syntha will auto-detect entities in the query and use them as filter.
    

### Example – Basic Search (GET):

You can perform a quick search with a GET request if you just have a simple query. For example:

```javascript
// Using fetch for a simple GET search
fetch(`https://${ZONE}.syntha.cloud/api/v1/kb/${KB_ID}/find?query=How to ride a bike`, {
  headers: {
    'X-SYNTHA-SERVICEACCOUNT': `Bearer ${API_KEY}`
  }
})
.then(res => res.json())
.then(data => {
  console.log('Search results:', data);
})
.catch(err => console.error('Error searching:', err));
```

### Example – Advanced Search (POST):

```javascript
// Using axios for an advanced search with POST
import axios from 'axios';  // (Ensure axios is imported in your environment)

const searchPayload = {
  query: "benefits of electric cars",
  features: ["semantic"],      // use only semantic search
  top_k: 5,                    // limit to 5 top results
  highlight: true,             // enable highlight of query terms in results
  filters: {
    // optional filters to narrow down results
    resource_type: ["document"],
    metadata: {
      category: ["transportation"]
    }
  }
};

axios.post(`https://${ZONE}.syntha.cloud/api/v1/kb/${KB_ID}/find`, searchPayload, {
  headers: {
    'X-SYNTHA-SERVICEACCOUNT': `Bearer ${API_KEY}`,
    'Content-Type': 'application/json'
  }
})
.then(response => {
  console.log('Advanced search results:', response.data);
})
.catch(error => console.error('Error with advanced search:', error));
```

### Best practices:

#### Pagination:

If you expect many results, use page\_number (and optionally a page size) to paginate through results. The API may return a default number of results if not specified (often the top 10 or 20).

#### Rate limits:

Each search request counts toward your account's rate limit (by default 2400 requests per minute across all API call. If you send a very high volume of search queries, you might hit this limit – in which case the API returns HTTP 429. Plan to throttle or batch queries as needed for high-traffic applications.

## /ask Endpoint

### Purpose:

Ask a question and get a generative answer based on your Knowledge Box data. The /ask endpoint is Syntha's Retrieval-Augmented Generation (RAG) API: it first finds relevant data (like the /find endpoint would) and then uses a language model (LLM) to generate an answer from that data. This is ideal for building Q&A features or chatbots that answer using your own documentation or knowledge base.

### Endpoint URL:

```
https://<ZONE>.syntha.cloud/api/v1/kb/<KB_ID>/ask
```

### Example – Asking a Question (POST):

The following example uses fetch to ask a question. We'll use the synchronous mode for easier handling, and request citation info.

```javascript
const questionPayload = {
  query: "Who is Hedy Lamarr and what is she known for?",
  context: [],               // no additional context in this example
  citations: true,           // include citations in the response
  synchronous: true,         // wait for the complete answer (not streaming)
  max_tokens: 500            // limit answer length
};

fetch(`https://${ZONE}.syntha.cloud/api/v1/kb/${KB_ID}/ask`, {
  method: 'POST',
  headers: {
    'X-SYNTHA-SERVICEACCOUNT': `Bearer ${API_KEY}`,
    'Content-Type': 'application/json'
  },
  body: JSON.stringify(questionPayload)
})
.then(res => res.json())
.then(data => {
  console.log('Answer:', data.answer);
  console.log('Citations:', data.citations);
  console.log('Retrieved context:', data.context);
})
.catch(err => console.error('Error asking question:', err));
```

### Best practices:

When using /ask, always verify the answer if critical – while Syntha's approach minimizes hallucinations by grounding in your data, no AI is 100% perfect. Use the retrieval context to display sources or to allow users to drill down into the original documents for verification. Enabling citations is a good practice to increase trust, as it clearly ties answer statements to source data. Also, keep an eye on the length of your answers and the number of context paragraphs – very long answers or supplying too many context documents could affect performance or costs; Syntha allows you to adjust max\_tokens and similar settings if needed.

### Rate limits:

The /ask calls are also subject to rate limiting. Given that each ask request is more resource-intensive (involves an LLM), avoid calling it in a tight loop. The default 2400 req/min account limit apply, but practically you may hit other usage limits depending on your plan or the complexity of queries. If you receive a 429 response, implement a retry strategy with backoff as mentioned earlier. Also note that very large payloads (lots of context or very long prompts) might be constrained by request size limits.

